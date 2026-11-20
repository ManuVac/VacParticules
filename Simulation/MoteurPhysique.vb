Imports System
Imports System.Collections.Generic
Imports System.Drawing

Public Class MoteurPhysique
    Private ReadOnly _rnd As New Random()

    Public ReadOnly Particules As New List(Of Particule)
    Public Chambre As New Chambre()
    Public CollisionsParois As Long
    Public CollisionsGaz As Long
    Public CollisionsParticuleParticule As Long
    Public ActiverCollisionsPP As Boolean
    Public ReflexionDiffuse As Boolean

    Public Sub Reinitialiser(nb As Integer, largeurPx As Double, hauteurPx As Double)
        Particules.Clear()
        CollisionsParois = 0
        CollisionsGaz = 0
        CollisionsParticuleParticule = 0
        For i As Integer = 1 To nb
            Particules.Add(CreerParticuleAleatoire(largeurPx, hauteurPx))
        Next
    End Sub

    Public Sub InjecterBouffee(xPx As Double, yPx As Double, nb As Integer, largeurPx As Double, hauteurPx As Double)
        For i As Integer = 1 To nb
            Dim p = CreerParticuleAleatoire(largeurPx, hauteurPx)
            p.X = xPx + Gauss() * 12.0
            p.Y = yPx + Gauss() * 12.0
            LimiterPosition(p, largeurPx, hauteurPx)
            Particules.Add(p)
        Next
    End Sub

    Public Sub Avancer(dtReel As Double, timeScale As Double, largeurPx As Double, hauteurPx As Double)
        Dim dt = dtReel * timeScale
        If dt <= 0.0 OrElse Particules.Count = 0 Then Return

        Dim ech = Echelle(largeurPx)
        Dim lambdaPx = ConstantesPhysique.LibreParcoursMoyen(Chambre.PressionMbar, Chambre.TemperatureK) * ech

        Dim vMax = 1.0
        For Each p In Particules
            Dim v = p.Vitesse()
            If v > vMax Then vMax = v
        Next

        Dim deplMax = vMax * dt
        Dim minDim = Math.Min(largeurPx, hauteurPx)
        Dim nbSousPas = CInt(Math.Ceiling(deplMax / (0.25 * minDim)))
        If nbSousPas < 1 Then nbSousPas = 1
        If nbSousPas > 400 Then nbSousPas = 400
        Dim h = dt / nbSousPas

        For s As Integer = 1 To nbSousPas
            For Each p In Particules
                If Not Double.IsPositiveInfinity(lambdaPx) Then
                    Dim proba = 1.0 - Math.Exp(-(p.Vitesse() * h) / lambdaPx)
                    If _rnd.NextDouble() < proba Then
                        Dim v = p.Vitesse()
                        Dim a = 2.0 * Math.PI * _rnd.NextDouble()
                        p.Vx = v * Math.Cos(a)
                        p.Vy = v * Math.Sin(a)
                        CollisionsGaz += 1
                    End If
                End If
                p.X += p.Vx * h
                p.Y += p.Vy * h
                GererParoi(p, largeurPx, hauteurPx)
            Next
            If ActiverCollisionsPP Then GererCollisionsPP()
        Next
    End Sub

    Private Function CreerParticuleAleatoire(largeurPx As Double, hauteurPx As Double) As Particule
        Dim ech = Echelle(largeurPx)
        Dim sigmaM = Math.Sqrt(ConstantesPhysique.kB * Chambre.TemperatureK / ConstantesPhysique.MasseN2)
        Dim p As New Particule()
        p.X = _rnd.NextDouble() * largeurPx
        p.Y = _rnd.NextDouble() * hauteurPx
        p.Vx = Gauss() * sigmaM * ech
        p.Vy = Gauss() * sigmaM * ech
        Return p
    End Function

    Private Function Echelle(largeurPx As Double) As Double
        Return largeurPx / Chambre.LargeurM
    End Function

    Private Function Gauss() As Double
        Dim u1 = 1.0 - _rnd.NextDouble()
        Dim u2 = _rnd.NextDouble()
        Return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2)
    End Function

    Private Sub GererParoi(p As Particule, w As Double, h As Double)
        Dim r = p.Rayon
        If p.X < r Then
            p.X = r
            If ReflexionDiffuse Then ReflechirDiffus(p, True, 1) Else p.Vx = Math.Abs(p.Vx)
            CollisionsParois += 1
        ElseIf p.X > w - r Then
            p.X = w - r
            If ReflexionDiffuse Then ReflechirDiffus(p, True, -1) Else p.Vx = -Math.Abs(p.Vx)
            CollisionsParois += 1
        End If
        If p.Y < r Then
            p.Y = r
            If ReflexionDiffuse Then ReflechirDiffus(p, False, 1) Else p.Vy = Math.Abs(p.Vy)
            CollisionsParois += 1
        ElseIf p.Y > h - r Then
            p.Y = h - r
            If ReflexionDiffuse Then ReflechirDiffus(p, False, -1) Else p.Vy = -Math.Abs(p.Vy)
            CollisionsParois += 1
        End If
    End Sub

    Private Sub ReflechirDiffus(p As Particule, horizontal As Boolean, sens As Integer)
        Dim v = p.Vitesse()
        Dim phi = Math.Asin(2.0 * _rnd.NextDouble() - 1.0)
        If horizontal Then
            p.Vx = sens * v * Math.Cos(phi)
            p.Vy = v * Math.Sin(phi)
        Else
            p.Vy = sens * v * Math.Cos(phi)
            p.Vx = v * Math.Sin(phi)
        End If
    End Sub

    Private Sub GererCollisionsPP()
        Dim n = Particules.Count
        For i As Integer = 0 To n - 2
            Dim a = Particules(i)
            For j As Integer = i + 1 To n - 1
                Dim b = Particules(j)
                Dim dx = b.X - a.X
                Dim dy = b.Y - a.Y
                Dim rs = a.Rayon + b.Rayon
                Dim d2 = dx * dx + dy * dy
                If d2 < rs * rs Then
                    Dim d = Math.Sqrt(d2)
                    If d < 1.0E-9 Then d = 1.0E-9
                    Dim nx = dx / d
                    Dim ny = dy / d
                    Dim vn = (b.Vx - a.Vx) * nx + (b.Vy - a.Vy) * ny
                    If vn < 0.0 Then
                        a.Vx += vn * nx
                        a.Vy += vn * ny
                        b.Vx -= vn * nx
                        b.Vy -= vn * ny
                        CollisionsParticuleParticule += 1
                    End If
                    Dim chevauchement = rs - d
                    a.X -= nx * chevauchement * 0.5
                    a.Y -= ny * chevauchement * 0.5
                    b.X += nx * chevauchement * 0.5
                    b.Y += ny * chevauchement * 0.5
                End If
            Next
        Next
    End Sub

    Private Sub LimiterPosition(p As Particule, w As Double, h As Double)
        If p.X < p.Rayon Then p.X = p.Rayon
        If p.X > w - p.Rayon Then p.X = w - p.Rayon
        If p.Y < p.Rayon Then p.Y = p.Rayon
        If p.Y > h - p.Rayon Then p.Y = h - p.Rayon
    End Sub
End Class
