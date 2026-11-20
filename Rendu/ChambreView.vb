Imports System
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class ChambreView
    Inherits UserControl

    Private Const MargePx As Integer = 16
    Private Const EpaisseurParoi As Integer = 10
    Private Const ElevationDeg As Double = 30.0
    Private ReadOnly _couleurFond As Color = Color.FromArgb(13, 20, 32)

    Private m_moteur As MoteurPhysique
    Private m_trail As Bitmap
    Private m_montrerTrainees As Boolean
    Private m_colorerVitesse As Boolean = True
    Private m_vue3D As Boolean = True
    Private m_azimutDeg As Integer = 45

    Public Sub New()
        DoubleBuffered = True
        SetStyle(ControlStyles.AllPaintingInWmPaint Or ControlStyles.OptimizedDoubleBuffer Or ControlStyles.UserPaint Or ControlStyles.ResizeRedraw, True)
        BackColor = _couleurFond
    End Sub

    Public Property Moteur As MoteurPhysique
        Get
            Return m_moteur
        End Get
        Set(value As MoteurPhysique)
            m_moteur = value
        End Set
    End Property

    Public Property MontrerTrainees As Boolean
        Get
            Return m_montrerTrainees
        End Get
        Set(value As Boolean)
            m_montrerTrainees = value
            EffacerTrail()
        End Set
    End Property

    Public Property ColorerVitesse As Boolean
        Get
            Return m_colorerVitesse
        End Get
        Set(value As Boolean)
            m_colorerVitesse = value
        End Set
    End Property

    Public Property Vue3D As Boolean
        Get
            Return m_vue3D
        End Get
        Set(value As Boolean)
            m_vue3D = value
            EffacerTrail()
        End Set
    End Property

    Public Property AzimutDeg As Integer
        Get
            Return m_azimutDeg
        End Get
        Set(value As Integer)
            m_azimutDeg = value
        End Set
    End Property

    Public ReadOnly Property RectangleInterieur As Rectangle
        Get
            Dim m = MargePx + EpaisseurParoi
            Return New Rectangle(m, m, Math.Max(10, Width - 2 * m), Math.Max(10, Height - 2 * m))
        End Get
    End Property

    Public Sub EffacerTrail()
        If m_trail IsNot Nothing Then
            m_trail.Dispose()
            m_trail = Nothing
        End If
    End Sub

    Protected Overrides Sub OnResize(e As EventArgs)
        MyBase.OnResize(e)
        EffacerTrail()
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)
        If ClientSize.Width < 80 OrElse ClientSize.Height < 80 Then Return
        Dim g = e.Graphics
        g.SmoothingMode = SmoothingMode.AntiAlias

        If m_montrerTrainees Then
            If m_trail Is Nothing OrElse m_trail.Size <> ClientSize Then
                If m_trail IsNot Nothing Then m_trail.Dispose()
                m_trail = New Bitmap(Math.Max(1, ClientSize.Width), Math.Max(1, ClientSize.Height))
                Using gt = Graphics.FromImage(m_trail)
                    gt.Clear(_couleurFond)
                End Using
            End If
            Using gt = Graphics.FromImage(m_trail)
                Using bFade = New SolidBrush(Color.FromArgb(28, _couleurFond))
                    gt.FillRectangle(bFade, 0, 0, m_trail.Width, m_trail.Height)
                End Using
                DessinerScene(gt)
            End Using
            g.DrawImageUnscaled(m_trail, 0, 0)
        Else
            Using bFond = New SolidBrush(_couleurFond)
                g.FillRectangle(bFond, ClientRectangle)
            End Using
            DessinerScene(g)
        End If

        DessinerInfos(g)
    End Sub

    Private Sub DessinerScene(g As Graphics)
        If m_moteur Is Nothing Then Return
        If m_vue3D Then
            DessinerScene3D(g)
        Else
            DessinerScene2D(g)
        End If
    End Sub

    Private Function Echelle2D() As Double
        If m_moteur Is Nothing Then Return 1.0
        Dim r = RectangleInterieur
        Return Math.Min(r.Width / m_moteur.Chambre.LargeurM, r.Height / m_moteur.Chambre.ProfondeurM)
    End Function

    Public Function Projette2D(xM As Double, yM As Double) As PointF
        Dim r = RectangleInterieur
        Dim e = Echelle2D()
        Dim cx = r.X + (r.Width - m_moteur.Chambre.LargeurM * e) / 2.0
        Dim cy = r.Y + (r.Height - m_moteur.Chambre.ProfondeurM * e) / 2.0
        Return New PointF(CSng(cx + xM * e), CSng(cy + yM * e))
    End Function

    Private Sub CalculVue3D(ByRef e As Double, ByRef cx As Double, ByRef cy As Double)
        Dim r = RectangleInterieur
        Dim ch = m_moteur.Chambre
        Dim az = m_azimutDeg * Math.PI / 180.0
        Dim cosA = Math.Abs(Math.Cos(az))
        Dim sinA = Math.Abs(Math.Sin(az))
        Dim sinElev = Math.Sin(ElevationDeg * Math.PI / 180.0)
        Dim largM = ch.LargeurM * cosA + ch.ProfondeurM * sinA + 0.05
        Dim hautM = (ch.LargeurM * sinA + ch.ProfondeurM * cosA) * sinElev + ch.HauteurM + 0.05
        e = Math.Min(r.Width / largM, r.Height / hautM)
        cx = r.X + r.Width / 2.0
        cy = r.Y + (r.Height + ch.HauteurM * e) / 2.0
    End Sub

    Public Function Projette3D(xM As Double, yM As Double, zM As Double) As PointF
        Dim az = m_azimutDeg * Math.PI / 180.0
        Dim e As Double = 0.0
        Dim cx As Double = 0.0
        Dim cy As Double = 0.0
        CalculVue3D(e, cx, cy)
        Dim sinElev = Math.Sin(ElevationDeg * Math.PI / 180.0)
        Dim sx = cx + (xM * Math.Cos(az) - yM * Math.Sin(az)) * e
        Dim sy = cy + (xM * Math.Sin(az) + yM * Math.Cos(az)) * e * sinElev - zM * e
        Return New PointF(CSng(sx), CSng(sy))
    End Function

    Public Function ClicVersChambre(xPx As Integer, yPx As Integer) As Double()
        If m_moteur Is Nothing Then Return New Double() {0.0, 0.0, 0.0}
        Dim ch = m_moteur.Chambre
        If Not m_vue3D Then
            Dim r = RectangleInterieur
            Dim e = Echelle2D()
            Dim cx = r.X + (r.Width - ch.LargeurM * e) / 2.0
            Dim cy = r.Y + (r.Height - ch.ProfondeurM * e) / 2.0
            Dim x = (xPx - cx) / e
            Dim y = (yPx - cy) / e
            If x < 0 Then x = 0
            If x > ch.LargeurM Then x = ch.LargeurM
            If y < 0 Then y = 0
            If y > ch.ProfondeurM Then y = ch.ProfondeurM
            Return New Double() {x, y, ch.HauteurM / 2.0}
        End If
        Dim az = m_azimutDeg * Math.PI / 180.0
        Dim e As Double = 0.0
        Dim cx As Double = 0.0
        Dim cy As Double = 0.0
        CalculVue3D(e, cx, cy)
        Dim sinElev = Math.Sin(ElevationDeg * Math.PI / 180.0)
        Dim z = ch.HauteurM / 2.0
        Dim a1 = (xPx - cx) / e
        Dim a2 = (yPx - cy + z * e) / (e * sinElev)
        Dim x = a1 * Math.Cos(az) + a2 * Math.Sin(az)
        Dim y = -a1 * Math.Sin(az) + a2 * Math.Cos(az)
        If x < 0 Then x = 0
        If x > ch.LargeurM Then x = ch.LargeurM
        If y < 0 Then y = 0
        If y > ch.ProfondeurM Then y = ch.ProfondeurM
        Return New Double() {x, y, z}
    End Function

    Private Sub DessinerScene2D(g As Graphics)
        Dim e = Echelle2D()
        For Each p In m_moteur.Particules
            Dim sp = Projette2D(p.X, p.Y)
            Dim rp = CSng(Math.Max(1.5, p.Rayon * e))
            DessinerParticule(g, sp, rp, CouleurParticule(p))
        Next
        DessinerParois2D(g)
    End Sub

    Private Sub DessinerParois2D(g As Graphics)
        Dim p0 = Projette2D(0.0, 0.0)
        Dim p1 = Projette2D(m_moteur.Chambre.LargeurM, m_moteur.Chambre.ProfondeurM)
        Dim rect = RectangleF.FromLTRB(p0.X, p0.Y, p1.X, p1.Y)
        Using p = New Pen(Color.FromArgb(150, 160, 175), EpaisseurParoi)
            p.LineJoin = LineJoin.Round
            g.DrawRectangle(p, rect)
        End Using
        Using p2 = New Pen(Color.FromArgb(60, 220, 230, 240), 2)
            Dim r2 = RectangleF.Inflate(rect, -EpaisseurParoi / 2.0F - 1.0F, -EpaisseurParoi / 2.0F - 1.0F)
            g.DrawRectangle(p2, r2)
        End Using
    End Sub

    Private Sub DessinerScene3D(g As Graphics)
        Dim ch = m_moteur.Chambre
        Dim az = m_azimutDeg * Math.PI / 180.0
        Dim cosA = Math.Cos(az)
        Dim sinA = Math.Sin(az)
        Dim L = ch.LargeurM
        Dim P = ch.ProfondeurM
        Dim H = ch.HauteurM

        Dim coinsM(7, 2) As Double
        coinsM(0, 0) = 0 : coinsM(0, 1) = 0 : coinsM(0, 2) = 0
        coinsM(1, 0) = L : coinsM(1, 1) = 0 : coinsM(1, 2) = 0
        coinsM(2, 0) = L : coinsM(2, 1) = P : coinsM(2, 2) = 0
        coinsM(3, 0) = 0 : coinsM(3, 1) = P : coinsM(3, 2) = 0
        coinsM(4, 0) = 0 : coinsM(4, 1) = 0 : coinsM(4, 2) = H
        coinsM(5, 0) = L : coinsM(5, 1) = 0 : coinsM(5, 2) = H
        coinsM(6, 0) = L : coinsM(6, 1) = P : coinsM(6, 2) = H
        coinsM(7, 0) = 0 : coinsM(7, 1) = P : coinsM(7, 2) = H

        Dim coinsP(7) As PointF
        For i As Integer = 0 To 7
            coinsP(i) = Projette3D(coinsM(i, 0), coinsM(i, 1), coinsM(i, 2))
        Next

        Dim e As Double = 0.0
        Dim cx As Double = 0.0
        Dim cy As Double = 0.0
        CalculVue3D(e, cx, cy)

        Dim faceVis(5) As Boolean
        faceVis(0) = False
        faceVis(1) = True
        faceVis(2) = cosA > 0
        faceVis(3) = cosA < 0
        faceVis(4) = sinA > 0
        faceVis(5) = sinA < 0

        Dim aretes(11, 3) As Integer
        aretes(0, 0) = 0 : aretes(0, 1) = 1 : aretes(0, 2) = 0 : aretes(0, 3) = 5
        aretes(1, 0) = 1 : aretes(1, 1) = 2 : aretes(1, 2) = 0 : aretes(1, 3) = 2
        aretes(2, 0) = 2 : aretes(2, 1) = 3 : aretes(2, 2) = 0 : aretes(2, 3) = 4
        aretes(3, 0) = 3 : aretes(3, 1) = 0 : aretes(3, 2) = 0 : aretes(3, 3) = 3
        aretes(4, 0) = 4 : aretes(4, 1) = 5 : aretes(4, 2) = 1 : aretes(4, 3) = 5
        aretes(5, 0) = 5 : aretes(5, 1) = 6 : aretes(5, 2) = 1 : aretes(5, 3) = 2
        aretes(6, 0) = 6 : aretes(6, 1) = 7 : aretes(6, 2) = 1 : aretes(6, 3) = 4
        aretes(7, 0) = 7 : aretes(7, 1) = 4 : aretes(7, 2) = 1 : aretes(7, 3) = 3
        aretes(8, 0) = 0 : aretes(8, 1) = 4 : aretes(8, 2) = 3 : aretes(8, 3) = 5
        aretes(9, 0) = 1 : aretes(9, 1) = 5 : aretes(9, 2) = 2 : aretes(9, 3) = 5
        aretes(10, 0) = 2 : aretes(10, 1) = 6 : aretes(10, 2) = 2 : aretes(10, 3) = 4
        aretes(11, 0) = 3 : aretes(11, 1) = 7 : aretes(11, 2) = 3 : aretes(11, 3) = 4

        Using pCache = New Pen(Color.FromArgb(70, 95, 105, 115), 1.5F)
            For i As Integer = 0 To 11
                If Not faceVis(aretes(i, 2)) AndAlso Not faceVis(aretes(i, 3)) Then
                    g.DrawLine(pCache, coinsP(aretes(i, 0)), coinsP(aretes(i, 1)))
                End If
            Next
        End Using

        Dim liste = New List(Of Particule)(m_moteur.Particules)
        liste.Sort(Function(a, b) Profondeur(a, cosA, sinA).CompareTo(Profondeur(b, cosA, sinA)))
        For Each p In liste
            Dim sp = Projette3D(p.X, p.Y, p.Z)
            Dim rp = CSng(Math.Max(1.5, p.Rayon * e))
            DessinerParticule(g, sp, rp, CouleurParticule(p))
        Next

        Using bFace = New SolidBrush(Color.FromArgb(38, 170, 180, 195))
            If faceVis(1) Then g.FillPolygon(bFace, New PointF() {coinsP(4), coinsP(5), coinsP(6), coinsP(7)})
            If faceVis(2) Then g.FillPolygon(bFace, New PointF() {coinsP(1), coinsP(2), coinsP(6), coinsP(5)})
            If faceVis(3) Then g.FillPolygon(bFace, New PointF() {coinsP(0), coinsP(3), coinsP(7), coinsP(4)})
            If faceVis(4) Then g.FillPolygon(bFace, New PointF() {coinsP(2), coinsP(3), coinsP(7), coinsP(6)})
            If faceVis(5) Then g.FillPolygon(bFace, New PointF() {coinsP(0), coinsP(1), coinsP(5), coinsP(4)})
        End Using

        Using pVis = New Pen(Color.FromArgb(220, 200, 210, 225), 2.0F)
            pVis.LineJoin = LineJoin.Round
            For i As Integer = 0 To 11
                If faceVis(aretes(i, 2)) OrElse faceVis(aretes(i, 3)) Then
                    g.DrawLine(pVis, coinsP(aretes(i, 0)), coinsP(aretes(i, 1)))
                End If
            Next
        End Using
    End Sub

    Private Shared Function Profondeur(p As Particule, cosA As Double, sinA As Double) As Double
        Return p.X * cosA + p.Y * sinA
    End Function

    Private Function CouleurParticule(p As Particule) As Color
        If Not m_colorerVitesse OrElse m_moteur Is Nothing Then Return p.Couleur
        Dim vRms = ConstantesPhysique.VitesseRms(m_moteur.Chambre.TemperatureK)
        If vRms <= 0.0 Then Return p.Couleur
        Return CouleurPourVitesse(p.Vitesse() / (2.0 * vRms))
    End Function

    Private Sub DessinerParticule(g As Graphics, sp As PointF, rp As Single, coul As Color)
        Using bHalo = New SolidBrush(Color.FromArgb(50, coul))
            g.FillEllipse(bHalo, sp.X - rp * 2.2F, sp.Y - rp * 2.2F, rp * 4.4F, rp * 4.4F)
        End Using
        Using bNoyau = New SolidBrush(coul)
            g.FillEllipse(bNoyau, sp.X - rp, sp.Y - rp, rp * 2.0F, rp * 2.0F)
        End Using
    End Sub

    Private Sub DessinerInfos(g As Graphics)
        If m_moteur Is Nothing Then Return
        Dim ch = m_moteur.Chambre
        Dim r = RectangleInterieur
        Dim txt = "chambre " & ch.LargeurM.ToString("0.00") & " x " & ch.ProfondeurM.ToString("0.00") & " x " & ch.HauteurM.ToString("0.00") & " m    " & ch.PressionMbar.ToString("0.0E+00") & " mbar"
        Using b = New SolidBrush(Color.FromArgb(110, 160, 180, 200))
            g.DrawString(txt, Font, b, r.X + 8, r.Y + 6)
        End Using
    End Sub

    Private Shared Function CouleurPourVitesse(t As Double) As Color
        If t < 0.0 Then t = 0.0
        If t > 1.0 Then t = 1.0
        Dim r, g, b As Integer
        If t < 0.5 Then
            Dim u = t * 2.0
            r = CInt(30 + u * (0 - 30))
            g = CInt(80 + u * (220 - 80))
            b = CInt(255 + u * (120 - 255))
        Else
            Dim u = (t - 0.5) * 2.0
            r = CInt(0 + u * 255)
            g = CInt(220 + u * (60 - 220))
            b = CInt(120 + u * (40 - 120))
        End If
        Return Color.FromArgb(255, r, g, b)
    End Function
End Class
