<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        components = New System.ComponentModel.Container()
        SplitContainer1 = New System.Windows.Forms.SplitContainer()
        ChambreView1 = New ChambreView()
        FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        GroupBoxCond = New System.Windows.Forms.GroupBox()
        LabelPressionLbl = New System.Windows.Forms.Label()
        TrackBarPression = New System.Windows.Forms.TrackBar()
        LabelPressionVal = New System.Windows.Forms.Label()
        LabelTempLbl = New System.Windows.Forms.Label()
        TrackBarTemp = New System.Windows.Forms.TrackBar()
        LabelTempVal = New System.Windows.Forms.Label()
        LabelInfo = New System.Windows.Forms.Label()
        GroupBoxParticules = New System.Windows.Forms.GroupBox()
        LabelNb = New System.Windows.Forms.Label()
        NumericUpDownNb = New System.Windows.Forms.NumericUpDown()
        BtnReinit = New System.Windows.Forms.Button()
        ChkCollisionsPP = New System.Windows.Forms.CheckBox()
        ChkDiffuse = New System.Windows.Forms.CheckBox()
        ChkTrainees = New System.Windows.Forms.CheckBox()
        ChkCouleur = New System.Windows.Forms.CheckBox()
        GroupBoxSimu = New System.Windows.Forms.GroupBox()
        GroupBoxVue = New System.Windows.Forms.GroupBox()
        ChkVue3D = New System.Windows.Forms.CheckBox()
        LabelAzimutLbl = New System.Windows.Forms.Label()
        TrackBarAzimut = New System.Windows.Forms.TrackBar()
        LabelAzimutVal = New System.Windows.Forms.Label()
        LabelVitesseLbl = New System.Windows.Forms.Label()
        TrackBarVitesse = New System.Windows.Forms.TrackBar()
        LabelVitesseVal = New System.Windows.Forms.Label()
        BtnMarchePause = New System.Windows.Forms.Button()
        LabelStats = New System.Windows.Forms.Label()
        LabelAide = New System.Windows.Forms.Label()
        Timer1 = New System.Windows.Forms.Timer(components)
        SplitContainer1.Panel1.SuspendLayout()
        SplitContainer1.Panel2.SuspendLayout()
        CType(SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        SplitContainer1.SuspendLayout()
        FlowLayoutPanel1.SuspendLayout()
        GroupBoxCond.SuspendLayout()
        CType(TrackBarPression, System.ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxParticules.SuspendLayout()
        CType(NumericUpDownNb, System.ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxVue.SuspendLayout()
        CType(TrackBarAzimut, System.ComponentModel.ISupportInitialize).BeginInit()
        GroupBoxSimu.SuspendLayout()
        CType(TrackBarVitesse, System.ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        '
        'SplitContainer1
        '
        SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        SplitContainer1.Location = New System.Drawing.Point(0, 0)
        SplitContainer1.Name = "SplitContainer1"
        SplitContainer1.Panel1.Controls.Add(ChambreView1)
        SplitContainer1.Panel1.Padding = New System.Windows.Forms.Padding(0)
        SplitContainer1.Panel2.Controls.Add(FlowLayoutPanel1)
        SplitContainer1.Panel2.Controls.Add(LabelAide)
        SplitContainer1.Panel2MinSize = 300
        SplitContainer1.Size = New System.Drawing.Size(1100, 640)
        SplitContainer1.SplitterDistance = 780
        SplitContainer1.SplitterWidth = 6
        SplitContainer1.TabIndex = 0
        '
        'ChambreView1
        '
        ChambreView1.ColorerVitesse = True
        ChambreView1.Dock = System.Windows.Forms.DockStyle.Fill
        ChambreView1.Location = New System.Drawing.Point(0, 0)
        ChambreView1.Moteur = Nothing
        ChambreView1.MontrerTrainees = False
        ChambreView1.Name = "ChambreView1"
        ChambreView1.Size = New System.Drawing.Size(780, 640)
        ChambreView1.TabIndex = 0
        '
        'FlowLayoutPanel1
        '
        FlowLayoutPanel1.AutoScroll = True
        FlowLayoutPanel1.Controls.Add(GroupBoxCond)
        FlowLayoutPanel1.Controls.Add(GroupBoxParticules)
        FlowLayoutPanel1.Controls.Add(GroupBoxVue)
        FlowLayoutPanel1.Controls.Add(GroupBoxSimu)
        FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(8)
        FlowLayoutPanel1.Size = New System.Drawing.Size(314, 606)
        FlowLayoutPanel1.TabIndex = 0
        FlowLayoutPanel1.WrapContents = False
        '
        'GroupBoxCond
        '
        GroupBoxCond.Controls.Add(LabelPressionLbl)
        GroupBoxCond.Controls.Add(TrackBarPression)
        GroupBoxCond.Controls.Add(LabelPressionVal)
        GroupBoxCond.Controls.Add(LabelTempLbl)
        GroupBoxCond.Controls.Add(TrackBarTemp)
        GroupBoxCond.Controls.Add(LabelTempVal)
        GroupBoxCond.Controls.Add(LabelInfo)
        GroupBoxCond.Location = New System.Drawing.Point(11, 11)
        GroupBoxCond.Name = "GroupBoxCond"
        GroupBoxCond.Size = New System.Drawing.Size(286, 238)
        GroupBoxCond.TabIndex = 0
        GroupBoxCond.TabStop = False
        GroupBoxCond.Text = "Conditions du vide"
        '
        'LabelPressionLbl
        '
        LabelPressionLbl.AutoSize = True
        LabelPressionLbl.Location = New System.Drawing.Point(10, 24)
        LabelPressionLbl.Name = "LabelPressionLbl"
        LabelPressionLbl.Size = New System.Drawing.Size(64, 15)
        LabelPressionLbl.TabIndex = 0
        LabelPressionLbl.Text = "Pression :"
        '
        'TrackBarPression
        '
        TrackBarPression.Location = New System.Drawing.Point(10, 42)
        TrackBarPression.Maximum = 150
        TrackBarPression.Name = "TrackBarPression"
        TrackBarPression.Size = New System.Drawing.Size(264, 45)
        TrackBarPression.TabIndex = 1
        TrackBarPression.TickFrequency = 15
        TrackBarPression.Value = 120
        '
        'LabelPressionVal
        '
        LabelPressionVal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point)
        LabelPressionVal.Location = New System.Drawing.Point(10, 90)
        LabelPressionVal.Name = "LabelPressionVal"
        LabelPressionVal.Size = New System.Drawing.Size(264, 20)
        LabelPressionVal.TabIndex = 2
        LabelPressionVal.Text = "1,0E-09 mbar"
        '
        'LabelTempLbl
        '
        LabelTempLbl.AutoSize = True
        LabelTempLbl.Location = New System.Drawing.Point(10, 118)
        LabelTempLbl.Name = "LabelTempLbl"
        LabelTempLbl.Size = New System.Drawing.Size(82, 15)
        LabelTempLbl.TabIndex = 3
        LabelTempLbl.Text = "Temperature :"
        '
        'TrackBarTemp
        '
        TrackBarTemp.Location = New System.Drawing.Point(10, 136)
        TrackBarTemp.Maximum = 1000
        TrackBarTemp.Minimum = 100
        TrackBarTemp.Name = "TrackBarTemp"
        TrackBarTemp.Size = New System.Drawing.Size(180, 45)
        TrackBarTemp.TabIndex = 4
        TrackBarTemp.TickFrequency = 100
        TrackBarTemp.Value = 293
        '
        'LabelTempVal
        '
        LabelTempVal.Location = New System.Drawing.Point(196, 144)
        LabelTempVal.Name = "LabelTempVal"
        LabelTempVal.Size = New System.Drawing.Size(70, 20)
        LabelTempVal.TabIndex = 5
        LabelTempVal.Text = "293 K"
        '
        'LabelInfo
        '
        LabelInfo.Location = New System.Drawing.Point(10, 186)
        LabelInfo.Name = "LabelInfo"
        LabelInfo.Size = New System.Drawing.Size(264, 46)
        LabelInfo.TabIndex = 6
        LabelInfo.Text = ""
        '
        'GroupBoxParticules
        '
        GroupBoxParticules.Controls.Add(LabelNb)
        GroupBoxParticules.Controls.Add(NumericUpDownNb)
        GroupBoxParticules.Controls.Add(BtnReinit)
        GroupBoxParticules.Controls.Add(ChkCollisionsPP)
        GroupBoxParticules.Controls.Add(ChkDiffuse)
        GroupBoxParticules.Controls.Add(ChkTrainees)
        GroupBoxParticules.Controls.Add(ChkCouleur)
        GroupBoxParticules.Location = New System.Drawing.Point(11, 255)
        GroupBoxParticules.Name = "GroupBoxParticules"
        GroupBoxParticules.Size = New System.Drawing.Size(286, 162)
        GroupBoxParticules.TabIndex = 1
        GroupBoxParticules.TabStop = False
        GroupBoxParticules.Text = "Particules"
        '
        'LabelNb
        '
        LabelNb.AutoSize = True
        LabelNb.Location = New System.Drawing.Point(10, 24)
        LabelNb.Name = "LabelNb"
        LabelNb.Size = New System.Drawing.Size(124, 15)
        LabelNb.TabIndex = 0
        LabelNb.Text = "Nombre de particules :"
        '
        'NumericUpDownNb
        '
        NumericUpDownNb.Location = New System.Drawing.Point(10, 44)
        NumericUpDownNb.Maximum = New Decimal(New Integer() {1000, 0, 0, 0})
        NumericUpDownNb.Minimum = New Decimal(New Integer() {10, 0, 0, 0})
        NumericUpDownNb.Name = "NumericUpDownNb"
        NumericUpDownNb.Size = New System.Drawing.Size(80, 23)
        NumericUpDownNb.TabIndex = 1
        NumericUpDownNb.Value = New Decimal(New Integer() {300, 0, 0, 0})
        '
        'BtnReinit
        '
        BtnReinit.Location = New System.Drawing.Point(100, 42)
        BtnReinit.Name = "BtnReinit"
        BtnReinit.Size = New System.Drawing.Size(120, 26)
        BtnReinit.TabIndex = 2
        BtnReinit.Text = "Reinitialiser"
        BtnReinit.UseVisualStyleBackColor = True
        '
        'ChkCollisionsPP
        '
        ChkCollisionsPP.AutoSize = True
        ChkCollisionsPP.Location = New System.Drawing.Point(10, 80)
        ChkCollisionsPP.Name = "ChkCollisionsPP"
        ChkCollisionsPP.Size = New System.Drawing.Size(181, 19)
        ChkCollisionsPP.TabIndex = 3
        ChkCollisionsPP.Text = "Collisions entre particules"
        ChkCollisionsPP.UseVisualStyleBackColor = True
        '
        'ChkDiffuse
        '
        ChkDiffuse.AutoSize = True
        ChkDiffuse.Location = New System.Drawing.Point(10, 104)
        ChkDiffuse.Name = "ChkDiffuse"
        ChkDiffuse.Size = New System.Drawing.Size(211, 19)
        ChkDiffuse.TabIndex = 4
        ChkDiffuse.Text = "Reflexion diffuse sur les parois"
        ChkDiffuse.UseVisualStyleBackColor = True
        '
        'ChkTrainees
        '
        ChkTrainees.AutoSize = True
        ChkTrainees.Location = New System.Drawing.Point(10, 130)
        ChkTrainees.Name = "ChkTrainees"
        ChkTrainees.Size = New System.Drawing.Size(67, 19)
        ChkTrainees.TabIndex = 5
        ChkTrainees.Text = "Trainees"
        ChkTrainees.UseVisualStyleBackColor = True
        '
        'ChkCouleur
        '
        ChkCouleur.AutoSize = True
        ChkCouleur.Checked = True
        ChkCouleur.CheckState = System.Windows.Forms.CheckState.Checked
        ChkCouleur.Location = New System.Drawing.Point(120, 130)
        ChkCouleur.Name = "ChkCouleur"
        ChkCouleur.Size = New System.Drawing.Size(152, 19)
        ChkCouleur.TabIndex = 6
        ChkCouleur.Text = "Couleur selon la vitesse"
        ChkCouleur.UseVisualStyleBackColor = True
        '
        'GroupBoxVue
        '
        GroupBoxVue.Controls.Add(ChkVue3D)
        GroupBoxVue.Controls.Add(LabelAzimutLbl)
        GroupBoxVue.Controls.Add(TrackBarAzimut)
        GroupBoxVue.Controls.Add(LabelAzimutVal)
        GroupBoxVue.Location = New System.Drawing.Point(11, 423)
        GroupBoxVue.Name = "GroupBoxVue"
        GroupBoxVue.Size = New System.Drawing.Size(286, 138)
        GroupBoxVue.TabIndex = 2
        GroupBoxVue.TabStop = False
        GroupBoxVue.Text = "Vue"
        '
        'ChkVue3D
        '
        ChkVue3D.AutoSize = True
        ChkVue3D.Checked = True
        ChkVue3D.CheckState = System.Windows.Forms.CheckState.Checked
        ChkVue3D.Location = New System.Drawing.Point(10, 22)
        ChkVue3D.Name = "ChkVue3D"
        ChkVue3D.Size = New System.Drawing.Size(140, 19)
        ChkVue3D.TabIndex = 0
        ChkVue3D.Text = "Vue 3D isometrique"
        ChkVue3D.UseVisualStyleBackColor = True
        '
        'LabelAzimutLbl
        '
        LabelAzimutLbl.AutoSize = True
        LabelAzimutLbl.Location = New System.Drawing.Point(10, 48)
        LabelAzimutLbl.Name = "LabelAzimutLbl"
        LabelAzimutLbl.Size = New System.Drawing.Size(101, 15)
        LabelAzimutLbl.TabIndex = 1
        LabelAzimutLbl.Text = "Rotation (azimut) :"
        '
        'TrackBarAzimut
        '
        TrackBarAzimut.Location = New System.Drawing.Point(10, 66)
        TrackBarAzimut.Maximum = 360
        TrackBarAzimut.Name = "TrackBarAzimut"
        TrackBarAzimut.Size = New System.Drawing.Size(264, 45)
        TrackBarAzimut.TabIndex = 2
        TrackBarAzimut.TickFrequency = 30
        TrackBarAzimut.Value = 45
        '
        'LabelAzimutVal
        '
        LabelAzimutVal.Location = New System.Drawing.Point(10, 114)
        LabelAzimutVal.Name = "LabelAzimutVal"
        LabelAzimutVal.Size = New System.Drawing.Size(100, 20)
        LabelAzimutVal.TabIndex = 3
        LabelAzimutVal.Text = "45 deg"
        '
        'GroupBoxSimu
        '
        GroupBoxSimu.Controls.Add(LabelVitesseLbl)
        GroupBoxSimu.Controls.Add(TrackBarVitesse)
        GroupBoxSimu.Controls.Add(LabelVitesseVal)
        GroupBoxSimu.Controls.Add(BtnMarchePause)
        GroupBoxSimu.Controls.Add(LabelStats)
        GroupBoxSimu.Location = New System.Drawing.Point(11, 423)
        GroupBoxSimu.Name = "GroupBoxSimu"
        GroupBoxSimu.Size = New System.Drawing.Size(286, 176)
        GroupBoxSimu.TabIndex = 2
        GroupBoxSimu.TabStop = False
        GroupBoxSimu.Text = "Simulation"
        '
        'LabelVitesseLbl
        '
        LabelVitesseLbl.AutoSize = True
        LabelVitesseLbl.Location = New System.Drawing.Point(10, 24)
        LabelVitesseLbl.Name = "LabelVitesseLbl"
        LabelVitesseLbl.Size = New System.Drawing.Size(119, 15)
        LabelVitesseLbl.TabIndex = 0
        LabelVitesseLbl.Text = "Vitesse de simulation :"
        '
        'TrackBarVitesse
        '
        TrackBarVitesse.Location = New System.Drawing.Point(10, 42)
        TrackBarVitesse.Maximum = 100
        TrackBarVitesse.Name = "TrackBarVitesse"
        TrackBarVitesse.Size = New System.Drawing.Size(264, 45)
        TrackBarVitesse.TabIndex = 1
        TrackBarVitesse.TickFrequency = 10
        TrackBarVitesse.Value = 33
        '
        'LabelVitesseVal
        '
        LabelVitesseVal.Location = New System.Drawing.Point(10, 90)
        LabelVitesseVal.Name = "LabelVitesseVal"
        LabelVitesseVal.Size = New System.Drawing.Size(120, 20)
        LabelVitesseVal.TabIndex = 2
        LabelVitesseVal.Text = "x 0,10"
        '
        'BtnMarchePause
        '
        BtnMarchePause.Location = New System.Drawing.Point(10, 118)
        BtnMarchePause.Name = "BtnMarchePause"
        BtnMarchePause.Size = New System.Drawing.Size(100, 28)
        BtnMarchePause.TabIndex = 3
        BtnMarchePause.Text = "Pause"
        BtnMarchePause.UseVisualStyleBackColor = True
        '
        'LabelStats
        '
        LabelStats.Location = New System.Drawing.Point(118, 116)
        LabelStats.Name = "LabelStats"
        LabelStats.Size = New System.Drawing.Size(158, 55)
        LabelStats.TabIndex = 4
        LabelStats.Text = ""
        '
        'LabelAide
        '
        LabelAide.Dock = System.Windows.Forms.DockStyle.Bottom
        LabelAide.Location = New System.Drawing.Point(0, 606)
        LabelAide.Name = "LabelAide"
        LabelAide.Padding = New System.Windows.Forms.Padding(8, 0, 0, 0)
        LabelAide.Size = New System.Drawing.Size(314, 34)
        LabelAide.TabIndex = 1
        LabelAide.Text = "Clic dans la chambre : injecte une bouffee de gaz."
        LabelAide.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Timer1
        '
        Timer1.Enabled = True
        Timer1.Interval = 15
        '
        'Form1
        '
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        ClientSize = New System.Drawing.Size(1100, 640)
        Controls.Add(SplitContainer1)
        MinimumSize = New System.Drawing.Size(900, 500)
        Name = "Form1"
        StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Text = "VacParticules - chambre UHV"
        SplitContainer1.Panel1.ResumeLayout(False)
        SplitContainer1.Panel2.ResumeLayout(False)
        CType(SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        SplitContainer1.ResumeLayout(False)
        FlowLayoutPanel1.ResumeLayout(False)
        GroupBoxCond.ResumeLayout(False)
        GroupBoxCond.PerformLayout()
        CType(TrackBarPression, System.ComponentModel.ISupportInitialize).EndInit()
        GroupBoxParticules.ResumeLayout(False)
        GroupBoxParticules.PerformLayout()
        CType(NumericUpDownNb, System.ComponentModel.ISupportInitialize).EndInit()
        GroupBoxVue.ResumeLayout(False)
        GroupBoxVue.PerformLayout()
        CType(TrackBarAzimut, System.ComponentModel.ISupportInitialize).EndInit()
        GroupBoxSimu.ResumeLayout(False)
        GroupBoxSimu.PerformLayout()
        CType(TrackBarVitesse, System.ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ChambreView1 As ChambreView
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents GroupBoxCond As System.Windows.Forms.GroupBox
    Friend WithEvents LabelPressionLbl As System.Windows.Forms.Label
    Friend WithEvents TrackBarPression As System.Windows.Forms.TrackBar
    Friend WithEvents LabelPressionVal As System.Windows.Forms.Label
    Friend WithEvents LabelTempLbl As System.Windows.Forms.Label
    Friend WithEvents TrackBarTemp As System.Windows.Forms.TrackBar
    Friend WithEvents LabelTempVal As System.Windows.Forms.Label
    Friend WithEvents LabelInfo As System.Windows.Forms.Label
    Friend WithEvents GroupBoxParticules As System.Windows.Forms.GroupBox
    Friend WithEvents LabelNb As System.Windows.Forms.Label
    Friend WithEvents NumericUpDownNb As System.Windows.Forms.NumericUpDown
    Friend WithEvents BtnReinit As System.Windows.Forms.Button
    Friend WithEvents ChkCollisionsPP As System.Windows.Forms.CheckBox
    Friend WithEvents ChkDiffuse As System.Windows.Forms.CheckBox
    Friend WithEvents ChkTrainees As System.Windows.Forms.CheckBox
    Friend WithEvents ChkCouleur As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBoxSimu As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxVue As System.Windows.Forms.GroupBox
    Friend WithEvents ChkVue3D As System.Windows.Forms.CheckBox
    Friend WithEvents LabelAzimutLbl As System.Windows.Forms.Label
    Friend WithEvents TrackBarAzimut As System.Windows.Forms.TrackBar
    Friend WithEvents LabelAzimutVal As System.Windows.Forms.Label
    Friend WithEvents LabelVitesseLbl As System.Windows.Forms.Label
    Friend WithEvents TrackBarVitesse As System.Windows.Forms.TrackBar
    Friend WithEvents LabelVitesseVal As System.Windows.Forms.Label
    Friend WithEvents BtnMarchePause As System.Windows.Forms.Button
    Friend WithEvents LabelStats As System.Windows.Forms.Label
    Friend WithEvents LabelAide As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
