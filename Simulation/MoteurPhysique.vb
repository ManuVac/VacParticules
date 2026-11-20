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

    Public Sub Reinitialiser(nb As Integer)
        Particules.Clear()
        CollisionsParois = 0
        CollisionsGaz = 0
        CollisionsParticuleParticule = 0
        For i As Integer = 1 To nb
            Particules.Add(CreerParticuleAleatoire())
        Next
    End Sub

    Public Sub InjecterBouffee(xM As Double, yM As Double, zM As Double, nb As Integer)
        For i As Integer = 1 To nb
            Dim p = CreerParticuleAleatoire()
            p.X = xM + Gauss() * 0.01
            p.Y = yM + Gauss() * 0.01
            p.Z = zM + Gauss() * 0.01
            LimiterPosition(p)
            Particules.Add(p)
        Next
    End Sub

    Public Sub Avancer(dtReel As Double, timeScale As Double)
        Dim dt = dtReel * timeScale
        If dt <= 0.0 OrElse Particules.Count = 0 Then Return

        Dim lambdaM = ConstantesPhysique.LibreParcoursMoyen(Chambre.PressionMbar, Chambre.TemperatureK)

        Dim vMax = 1.0
        For Each p In Particules
            Dim v = p.Vitesse()
            If v > vMax Then vMax = v
        Next

        Dim minDimM = Math.Min(Chambre.LargeurM, Math.Min(Chambre.ProfondeurM, Chambre.HauteurM))
        Dim nbSousPas = CInt(Math.Ceiling(vMax * dt / (0.25 * minDimM)))
        If nbSousPas < 1 Then nbSousPas = 1
        If nbSousPas > 400 Then nbSousPas = 400
        Dim h = dt / nbSousPas

        For s As Integer = 1 To nbSousPas
            For Each p In Particules
                If Not Double.IsPositiveInfinity(lambdaM) Then
                    Dim proba = 1.0 - Math.Exp(-(p.Vitesse() * h) / lambdaM)
                    If _rnd.NextDouble() < proba Then
                        Dim v = p.Vitesse()
                        Dim a = 2.0 * Math.PI * _rnd.NextDouble()
                        Dim b = Math.Acos(2.0 * _rnd.NextDouble() - 1.0)
                        p.Vx = v * Math.Sin(b) * Math.Cos(a)
                        p.Vy = v * Math.Sin(b) * Math.Sin(a)
                        p.Vz = v * Math.Cos(b)
                        CollisionsGaz += 1
                    End If
                End If
                p.X += p.Vx * h
                p.Y += p.Vy * h
                p.Z += p.Vz * h
                GererParois(p)
            Next
            If ActiverCollisionsPP Then GererCollisionsPP()
        Next
    End Sub

    Private Function CreerParticuleAleatoire() As Particule
        Dim sigma = Math.Sqrt(ConstantesPhysique.kB * Chambre.TemperatureK / ConstantesPhysique.MasseN2)
        Dim p As New Particule()
        p.X = p.Rayon + _rnd.NextDouble() * Math.Max(0.0, Chambre.LargeurM - 2 * p.Rayon)
        p.Y = p.Rayon + _rnd.NextDouble() * Math.Max(0.0, Chambre.ProfondeurM - 2 * p.Rayon)
        p.Z = p.Rayon + _rnd.NextDouble() * Math.Max(0.0, Chambre.HauteurM - 2 * p.Rayon)
        p.Vx = Gauss() * sigma
        p.Vy = Gauss() * sigma
        p.Vz = Gauss() * sigma
        Return p
    End Function

    Private Function Gauss() As Double
        Dim u1 = 1.0 - _rnd.NextDouble()
        Dim u2 = _rnd.NextDouble()
        Return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Cos(2.0 * Math.PI * u2)
    End Function

    Private Sub GererParois(p As Particule)
        Dim r = p.Rayon
        If p.X < r Then
            p.X = r
            If ReflexionDiffuse Then ReflechirDiffus(p, 1, 0, 0) Else p.Vx = Math.Abs(p.Vx)
            CollisionsParois += 1
        ElseIf p.X > Chambre.LargeurM - r Then
            p.X = Chambre.LargeurM - r
            If ReflexionDiffuse Then ReflechirDiffus(p, -1, 0, 0) Else p.Vx = -Math.Abs(p.Vx)
            CollisionsParois += 1
        End If
        If p.Y < r Then
            p.Y = r
            If ReflexionDiffuse Then ReflechirDiffus(p, 0, 1, 0) Else p.Vy = Math.Abs(p.Vy)
            CollisionsParois += 1
        ElseIf p.Y > Chambre.ProfondeurM - r Then
            p.Y = Chambre.ProfondeurM - r
            If ReflexionDiffuse Then ReflechirDiffus(p, 0, -1, 0) Else p.Vy = -Math.Abs(p.Vy)
            CollisionsParois += 1
        End If
        If p.Z < r Then
            p.Z = r
            If ReflexionDiffuse Then ReflechirDiffus(p, 0, 0, 1) Else p.Vz = Math.Abs(p.Vz)
            CollisionsParois += 1
        ElseIf p.Z > Chambre.HauteurM - r Then
            p.Z = Chambre.HauteurM - r
            If ReflexionDiffuse Then ReflechirDiffus(p, 0, 0, -1) Else p.Vz = -Math.Abs(p.Vz)
            CollisionsParois += 1
        End If
    End Sub

    Private Sub ReflechirDiffus(p As Particule, nx As Double, ny As Double, nz As Double)
        Dim v = p.Vitesse()
        Dim phi = 2.0 * Math.PI * _rnd.NextDouble()
        Dim cosT = Math.Sqrt(_rnd.NextDouble())
        Dim sinT = Math.Sqrt(1.0 - cosT * cosT)
        Dim tx, ty, tz As Double
        If Math.Abs(nz) < 0.9 Then
            tx = -ny
            ty = nx
            tz = 0.0
        Else
            tx = 1.0
            ty = 0.0
            tz = 0.0
        End If
        Dim nt = Math.Sqrt(tx * tx + ty * ty + tz * tz)
        tx /= nt
        ty /= nt
        tz /= nt
        Dim ux = ny * tz - nz * ty
        Dim uy = nz * tx - nx * tz
        Dim uz = nx * ty - ny * tx
        Dim ca = Math.Cos(phi)
        Dim sa = Math.Sin(phi)
        p.Vx = v * (nx * cosT + (tx * ca + ux * sa) * sinT)
        p.Vy = v * (ny * cosT + (ty * ca + uy * sa) * sinT)
        p.Vz = v * (nz * cosT + (tz * ca + uz * sa) * sinT)
    End Sub

    Private Sub GererCollisionsPP()
        Dim n = Particules.Count
        For i As Integer = 0 To n - 2
            Dim a = Particules(i)
            For j As Integer = i + 1 To n - 1
                Dim b = Particules(j)
                Dim dx = b.X - a.X
                Dim dy = b.Y - a.Y
                Dim dz = b.Z - a.Z
                Dim rs = a.Rayon + b.Rayon
                Dim d2 = dx * dx + dy * dy + dz * dz
                If d2 < rs * rs Then
                    Dim d = Math.Sqrt(d2)
                    If d < 1.0E-12 Then d = 1.0E-12
                    Dim nx = dx / d
                    Dim ny = dy / d
                    Dim nz = dz / d
                    Dim vn = (b.Vx - a.Vx) * nx + (b.Vy - a.Vy) * ny + (b.Vz - a.Vz) * nz
                    If vn < 0.0 Then
                        a.Vx += vn * nx
                        a.Vy += vn * ny
                        a.Vz += vn * nz
                        b.Vx -= vn * nx
                        b.Vy -= vn * ny
                        b.Vz -= vn * nz
                        CollisionsParticuleParticule += 1
                    End If
                    Dim chev = rs - d
                    a.X -= nx * chev * 0.5
                    a.Y -= ny * chev * 0.5
                    a.Z -= nz * chev * 0.5
                    b.X += nx * chev * 0.5
                    b.Y += ny * chev * 0.5
                    b.Z += nz * chev * 0.5
                End If
            Next
        Next
    End Sub

    Private Sub LimiterPosition(p As Particule)
        If p.X < p.Rayon Then p.X = p.Rayon
        If p.X > Chambre.LargeurM - p.Rayon Then p.X = Chambre.LargeurM - p.Rayon
        If p.Y < p.Rayon Then p.Y = p.Rayon
        If p.Y > Chambre.ProfondeurM - p.Rayon Then p.Y = Chambre.ProfondeurM - p.Rayon
        If p.Z < p.Rayon Then p.Z = p.Rayon
        If p.Z > Chambre.HauteurM - p.Rayon Then p.Z = Chambre.HauteurM - p.Rayon
    End Sub
End Class
