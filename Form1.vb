Imports System
Imports System.Drawing
Imports System.Windows.Forms

Public Class Form1
    Private ReadOnly m_moteur As New MoteurPhysique()
    Private m_initialise As Boolean
    Private m_compteurFrames As Integer
    Private m_dernierFps As DateTime = DateTime.Now
    Private m_fps As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ChambreView1.Moteur = m_moteur
        m_moteur.Chambre.PressionMbar = PressionDepuisTrackBar()
        m_moteur.Chambre.TemperatureK = TrackBarTemp.Value
        ChambreView1.Vue3D = ChkVue3D.Checked
        ChambreView1.AzimutDeg = TrackBarAzimut.Value
        LabelAzimutVal.Text = TrackBarAzimut.Value & " deg"
        AppliquerOptions()
        MettreAJourInfos()
        MettreAJourVitesseLabel()
        m_initialise = True
        Reinitialiser()
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Reinitialiser()
    End Sub

    Private Function PressionDepuisTrackBar() As Double
        Return Math.Pow(10.0, 3.0 - TrackBarPression.Value / 10.0)
    End Function

    Private Function TimeScaleDepuisTrackBar() As Double
        Return Math.Pow(10.0, -2.0 + 3.0 * TrackBarVitesse.Value / 100.0)
    End Function

    Private Sub Reinitialiser()
        m_moteur.Reinitialiser(CInt(NumericUpDownNb.Value))
        ChambreView1.EffacerTrail()
    End Sub

    Private Sub AppliquerOptions()
        m_moteur.ActiverCollisionsPP = ChkCollisionsPP.Checked
        m_moteur.ReflexionDiffuse = ChkDiffuse.Checked
        ChambreView1.MontrerTrainees = ChkTrainees.Checked
        ChambreView1.ColorerVitesse = ChkCouleur.Checked
    End Sub

    Private Sub MettreAJourInfos()
        Dim p = m_moteur.Chambre.PressionMbar
        LabelPressionVal.Text = p.ToString("0.0E+00") & " mbar"
        LabelTempVal.Text = m_moteur.Chambre.TemperatureK.ToString("0") & " K"
        Dim lambda = ConstantesPhysique.LibreParcoursMoyen(p, m_moteur.Chambre.TemperatureK)
        Dim tailleM = Math.Max(m_moteur.Chambre.LargeurM, Math.Max(m_moteur.Chambre.ProfondeurM, m_moteur.Chambre.HauteurM))
        Dim txtLambda = ConstantesPhysique.FormaterDistance(lambda)
        If lambda > 100.0 * tailleM Then txtLambda &= "  (>> chambre : trajectoires libres)"
        LabelInfo.Text = "Regime : " & ConstantesPhysique.RegimeVide(p) & vbCrLf &
                         "Libre parcours moyen : " & txtLambda
    End Sub

    Private Sub MettreAJourVitesseLabel()
        LabelVitesseVal.Text = "x " & TimeScaleDepuisTrackBar().ToString("0.00")
    End Sub

    Private Sub MettreAJourStats()
        LabelStats.Text = "particules : " & m_moteur.Particules.Count & vbCrLf &
                          "parois : " & m_moteur.CollisionsParois & vbCrLf &
                          "gaz : " & m_moteur.CollisionsGaz & vbCrLf &
                          "inter-particules : " & m_moteur.CollisionsParticuleParticule & vbCrLf &
                          "FPS : " & m_fps
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        m_moteur.Avancer(Timer1.Interval / 1000.0, TimeScaleDepuisTrackBar())
        ChambreView1.Invalidate()
        m_compteurFrames += 1
        Dim maintenant = DateTime.Now
        Dim ecoule = (maintenant - m_dernierFps).TotalMilliseconds
        If ecoule >= 500.0 Then
            m_fps = CInt(m_compteurFrames * 1000.0 / ecoule)
            m_compteurFrames = 0
            m_dernierFps = maintenant
            MettreAJourStats()
        End If
    End Sub

    Private Sub TrackBarPression_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarPression.ValueChanged
        m_moteur.Chambre.PressionMbar = PressionDepuisTrackBar()
        MettreAJourInfos()
    End Sub

    Private Sub TrackBarTemp_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarTemp.ValueChanged
        m_moteur.Chambre.TemperatureK = TrackBarTemp.Value
        MettreAJourInfos()
    End Sub

    Private Sub TrackBarVitesse_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarVitesse.ValueChanged
        MettreAJourVitesseLabel()
    End Sub

    Private Sub TrackBarAzimut_ValueChanged(sender As Object, e As EventArgs) Handles TrackBarAzimut.ValueChanged
        ChambreView1.AzimutDeg = TrackBarAzimut.Value
        LabelAzimutVal.Text = TrackBarAzimut.Value & " deg"
    End Sub

    Private Sub ChkVue3D_CheckedChanged(sender As Object, e As EventArgs) Handles ChkVue3D.CheckedChanged
        ChambreView1.Vue3D = ChkVue3D.Checked
    End Sub

    Private Sub NumericUpDownNb_ValueChanged(sender As Object, e As EventArgs) Handles NumericUpDownNb.ValueChanged
        If m_initialise Then Reinitialiser()
    End Sub

    Private Sub BtnReinit_Click(sender As Object, e As EventArgs) Handles BtnReinit.Click
        Reinitialiser()
    End Sub

    Private Sub BtnMarchePause_Click(sender As Object, e As EventArgs) Handles BtnMarchePause.Click
        Timer1.Enabled = Not Timer1.Enabled
        BtnMarchePause.Text = If(Timer1.Enabled, "Pause", "Demarrer")
    End Sub

    Private Sub ChkOptions_CheckedChanged(sender As Object, e As EventArgs) Handles ChkCollisionsPP.CheckedChanged, ChkDiffuse.CheckedChanged, ChkTrainees.CheckedChanged, ChkCouleur.CheckedChanged
        AppliquerOptions()
    End Sub

    Private Sub ChambreView1_MouseDown(sender As Object, e As MouseEventArgs) Handles ChambreView1.MouseDown
        Dim c = ChambreView1.ClicVersChambre(e.X, e.Y)
        m_moteur.InjecterBouffee(c(0), c(1), c(2), 25)
    End Sub
End Class
