Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Windows.Forms

Public Class ChambreView
    Inherits UserControl

    Private Const MargePx As Integer = 16
    Private Const EpaisseurParoi As Integer = 10
    Private ReadOnly _couleurFond As Color = Color.FromArgb(13, 20, 32)

    Private m_moteur As MoteurPhysique
    Private m_trail As Bitmap
    Private m_montrerTrainees As Boolean
    Private m_colorerVitesse As Boolean = True

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

    Public ReadOnly Property RectangleInterieur As Rectangle
        Get
            Dim m = MargePx + EpaisseurParoi
            Return New Rectangle(m, m, Math.Max(10, Width - 2 * m), Math.Max(10, Height - 2 * m))
        End Get
    End Property

    Public ReadOnly Property EchellePxParM As Double
        Get
            If m_moteur Is Nothing Then Return 1.0
            Return RectangleInterieur.Width / m_moteur.Chambre.LargeurM
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
                DessinerParticules(gt)
            End Using
            g.DrawImageUnscaled(m_trail, 0, 0)
        Else
            Using bFond = New SolidBrush(_couleurFond)
                g.FillRectangle(bFond, ClientRectangle)
            End Using
            DessinerParticules(g)
        End If

        DessinerParois(g)
        DessinerInfos(g)
    End Sub

    Private Sub DessinerParticules(g As Graphics)
        If m_moteur Is Nothing Then Return
        Dim r = RectangleInterieur
        Dim vRmsPx = ConstantesPhysique.VitesseRms(m_moteur.Chambre.TemperatureK) * EchellePxParM
        For Each p In m_moteur.Particules
            Dim x = r.X + p.X
            Dim y = r.Y + p.Y
            Dim coul = p.Couleur
            If m_colorerVitesse AndAlso vRmsPx > 0.0 Then
                coul = CouleurPourVitesse(p.Vitesse() / (2.0 * vRmsPx))
            End If
            Using bHalo = New SolidBrush(Color.FromArgb(50, coul))
                g.FillEllipse(bHalo, CSng(x - p.Rayon * 2.2), CSng(y - p.Rayon * 2.2), CSng(p.Rayon * 4.4), CSng(p.Rayon * 4.4))
            End Using
            Using bNoyau = New SolidBrush(coul)
                g.FillEllipse(bNoyau, CSng(x - p.Rayon), CSng(y - p.Rayon), CSng(p.Rayon * 2.0), CSng(p.Rayon * 2.0))
            End Using
        Next
    End Sub

    Private Sub DessinerParois(g As Graphics)
        Dim d = EpaisseurParoi \ 2
        Dim w = Math.Max(1, Width - 2 * (MargePx + d))
        Dim h = Math.Max(1, Height - 2 * (MargePx + d))
        Dim rectParoi = New Rectangle(MargePx + d, MargePx + d, w, h)
        Using p = New Pen(Color.FromArgb(150, 160, 175), EpaisseurParoi)
            p.LineJoin = LineJoin.Round
            g.DrawRectangle(p, rectParoi)
        End Using
        Using p2 = New Pen(Color.FromArgb(60, 220, 230, 240), 2)
            Dim r2 = New Rectangle(rectParoi.X + EpaisseurParoi \ 2 + 1, rectParoi.Y + EpaisseurParoi \ 2 + 1, Math.Max(1, rectParoi.Width - EpaisseurParoi - 2), Math.Max(1, rectParoi.Height - EpaisseurParoi - 2))
            g.DrawRectangle(p2, r2)
        End Using
    End Sub

    Private Sub DessinerInfos(g As Graphics)
        If m_moteur Is Nothing Then Return
        Dim r = RectangleInterieur
        Dim txt = "chambre " & m_moteur.Chambre.LargeurM.ToString("0.00") & " m x " & m_moteur.Chambre.HauteurM.ToString("0.00") & " m    " & m_moteur.Chambre.PressionMbar.ToString("0.0E+00") & " mbar"
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
