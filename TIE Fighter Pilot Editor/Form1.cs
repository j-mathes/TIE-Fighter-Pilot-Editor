using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace TIE_Fighter_Pilot_Editor
{
    public partial class Form1 : Form
    {
        private bool validationPopUp = true;
        private string fileName = "";
        private byte[] bytes = null;
        private Pilot pilot;

        // Dictionaries containing grid view header names and their max values.
        // Used for cell validation

        Dictionary<string, int> gvTrainingMaxValueByHeaderTextDict
        = new Dictionary<string, int>() {
            { "NextTrainingLevel", 21 },
            { "TrainingScore", 2046820351 },
            { "TrainingLevelComp", 20 }
        };

        Dictionary<string, int> gvBattleVictoriesMaxValueByHeaderTextDict
        = new Dictionary<string, int>() {
            { "Victories", 65535 }
        };

        Dictionary<string, int> gvBattleScoresMaxValueByHeaderTextDict
        = new Dictionary<string, int>() {
            { "MissionScore", 65535 }
        };


        public Form1()
        {
            InitializeComponent();

            pilot = new Pilot();
            bytes = new byte[3856]; // First 1928 bytes are for active pilot.  Last 1928 bytes are for backup pilot data
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0x00;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Initialize control data sources here

            comHealth.DataSource = Enum.GetValues(typeof(Health));
            comRank.DataSource = Enum.GetValues(typeof(Rank));
            comDifficulty.DataSource = Enum.GetValues(typeof(Difficulty));
            comSecretOrderRank.DataSource = Enum.GetValues(typeof(SecretOrder));

            comStatusB1.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB2.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB3.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB4.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB5.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB6.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB7.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB8.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB9.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB10.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB11.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB12.DataSource = Enum.GetValues(typeof(BattleStatus));
            comStatusB13.DataSource = Enum.GetValues(typeof(BattleStatus));

            clbSecondaryObjectiveB1.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB2.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB3.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB4.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB5.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB6.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB7.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB8.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB9.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB10.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB11.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB12.DataSource = Enum.GetValues(typeof(SecondaryObjectives));
            clbSecondaryObjectiveB13.DataSource = Enum.GetValues(typeof(SecondaryObjectives));

            clbBonusObjectiveB1.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB2.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB3.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB4.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB5.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB6.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB7.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB8.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB9.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB10.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB11.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB12.DataSource = Enum.GetValues(typeof(BonusObjectives));
            clbBonusObjectiveB13.DataSource = Enum.GetValues(typeof(BonusObjectives));

            cbValidationToggle.Checked = validationPopUp;

            UpdateForm();
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            pilot = new Pilot();
            bytes = new byte[3856];
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = 0x00;
            }

            fileName = "";
            UpdateForm();
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "Pilot Files (*.tfr)|*.tfr";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = openFileDialog1.FileName;
                bytes = File.ReadAllBytes(fileName);

                if ((bytes.Length == 3856)) // equal to one more than the last index because the index starts at zero
                {
                    pilot.GetData(fileName, bytes);
                    UpdateForm();
                }
                else
                {
                    MessageBox.Show("The pilot file: " + fileName + " is not in the correct format for TIE Fighter 98.", "Wrong File Format");
                }
                UpdateForm();
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = fileName;
            saveFileDialog1.Filter = "Pilot Files (*.tfr)|*.tfr";
            saveFileDialog1.CheckFileExists = false;
            saveFileDialog1.CheckPathExists = true;

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog1.FileName;
                pilot.WriteData(fileName, bytes);
                File.WriteAllBytes(fileName, bytes);
                pilot.PltName = Path.GetFileNameWithoutExtension(fileName);
            }
            UpdateForm();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("TIE Fighter Pilot Editor v1.0" + Environment.NewLine + Environment.NewLine + "email: retrotek@shaw.ca", "About TIE Fighter Pilot Editor");
        }

        private void UpdateForm()
        {
            cbValidationToggle.Checked = validationPopUp;

            if (fileName != "")
            {
                toolStripStatusPilotName.Text = pilot.PltRank + " " + pilot.PltName + " is " + pilot.PltHealth + " with " + pilot.PltBattleScore + " Tour of Duty points.  " + pilot.PltRank + " " + pilot.PltName + " has a Secret Order rank of " + pilot.PltSecretOrderLevel + ".";
            }
            else
            {
                toolStripStatusPilotName.Text = "No Pilot File Loaded";
            }

            txtPilotName.Text = pilot.PltName;
            comHealth.SelectedItem = pilot.PltHealth;
            comRank.SelectedItem = pilot.PltRank;
            comDifficulty.SelectedItem = pilot.PltDifficulty;
            nudBattleScore.Value = pilot.PltBattleScore;
            nudSkill.Value = pilot.PltSkillLevel;
            comSecretOrderRank.SelectedItem = pilot.PltSecretOrderLevel;

            gvBattleVictories.DataSource = pilot.BattleStats.BattleVictoriesList;
            gvTraining.DataSource = pilot.TrainingRecordList;
            gvBattle1Scores.DataSource = pilot.ListOfBattles.BattlesList[0].MissionsList;
            gvBattle2Scores.DataSource = pilot.ListOfBattles.BattlesList[1].MissionsList;
            gvBattle3Scores.DataSource = pilot.ListOfBattles.BattlesList[2].MissionsList;
            gvBattle4Scores.DataSource = pilot.ListOfBattles.BattlesList[3].MissionsList;
            gvBattle5Scores.DataSource = pilot.ListOfBattles.BattlesList[4].MissionsList;
            gvBattle6Scores.DataSource = pilot.ListOfBattles.BattlesList[5].MissionsList;
            gvBattle7Scores.DataSource = pilot.ListOfBattles.BattlesList[6].MissionsList;
            gvBattle8Scores.DataSource = pilot.ListOfBattles.BattlesList[7].MissionsList;
            gvBattle9Scores.DataSource = pilot.ListOfBattles.BattlesList[8].MissionsList;
            gvBattle10Scores.DataSource = pilot.ListOfBattles.BattlesList[9].MissionsList;
            gvBattle11Scores.DataSource = pilot.ListOfBattles.BattlesList[10].MissionsList;
            gvBattle12Scores.DataSource = pilot.ListOfBattles.BattlesList[11].MissionsList;
            gvBattle13Scores.DataSource = pilot.ListOfBattles.BattlesList[12].MissionsList;

            gvTraining.Columns[0].ReadOnly = true;
            gvBattleVictories.Columns[0].ReadOnly = true;
            gvBattle1Scores.Columns[0].ReadOnly = true;
            gvBattle2Scores.Columns[0].ReadOnly = true;
            gvBattle3Scores.Columns[0].ReadOnly = true;
            gvBattle4Scores.Columns[0].ReadOnly = true;
            gvBattle5Scores.Columns[0].ReadOnly = true;
            gvBattle6Scores.Columns[0].ReadOnly = true;
            gvBattle7Scores.Columns[0].ReadOnly = true;
            gvBattle8Scores.Columns[0].ReadOnly = true;
            gvBattle9Scores.Columns[0].ReadOnly = true;
            gvBattle10Scores.Columns[0].ReadOnly = true;
            gvBattle11Scores.Columns[0].ReadOnly = true;
            gvBattle12Scores.Columns[0].ReadOnly = true;
            gvBattle13Scores.Columns[0].ReadOnly = true;

            comStatusB1.SelectedItem = pilot.ListOfBattles.BattlesList[0].Status;
            comStatusB2.SelectedItem = pilot.ListOfBattles.BattlesList[1].Status;
            comStatusB3.SelectedItem = pilot.ListOfBattles.BattlesList[2].Status;
            comStatusB4.SelectedItem = pilot.ListOfBattles.BattlesList[3].Status;
            comStatusB5.SelectedItem = pilot.ListOfBattles.BattlesList[4].Status;
            comStatusB6.SelectedItem = pilot.ListOfBattles.BattlesList[5].Status;
            comStatusB7.SelectedItem = pilot.ListOfBattles.BattlesList[6].Status;
            comStatusB8.SelectedItem = pilot.ListOfBattles.BattlesList[7].Status;
            comStatusB9.SelectedItem = pilot.ListOfBattles.BattlesList[8].Status;
            comStatusB10.SelectedItem = pilot.ListOfBattles.BattlesList[9].Status;
            comStatusB11.SelectedItem = pilot.ListOfBattles.BattlesList[10].Status;
            comStatusB12.SelectedItem = pilot.ListOfBattles.BattlesList[11].Status;
            comStatusB13.SelectedItem = pilot.ListOfBattles.BattlesList[12].Status;

            nudLastMissionCompB1.Value = pilot.ListOfBattles.BattlesList[0].LastMissionCompleted;
            nudLastMissionCompB2.Value = pilot.ListOfBattles.BattlesList[1].LastMissionCompleted;
            nudLastMissionCompB3.Value = pilot.ListOfBattles.BattlesList[2].LastMissionCompleted;
            nudLastMissionCompB4.Value = pilot.ListOfBattles.BattlesList[3].LastMissionCompleted;
            nudLastMissionCompB5.Value = pilot.ListOfBattles.BattlesList[4].LastMissionCompleted;
            nudLastMissionCompB6.Value = pilot.ListOfBattles.BattlesList[5].LastMissionCompleted;
            nudLastMissionCompB7.Value = pilot.ListOfBattles.BattlesList[6].LastMissionCompleted;
            nudLastMissionCompB8.Value = pilot.ListOfBattles.BattlesList[7].LastMissionCompleted;
            nudLastMissionCompB9.Value = pilot.ListOfBattles.BattlesList[8].LastMissionCompleted;
            nudLastMissionCompB10.Value = pilot.ListOfBattles.BattlesList[9].LastMissionCompleted;
            nudLastMissionCompB11.Value = pilot.ListOfBattles.BattlesList[10].LastMissionCompleted;
            nudLastMissionCompB12.Value = pilot.ListOfBattles.BattlesList[11].LastMissionCompleted;
            nudLastMissionCompB13.Value = pilot.ListOfBattles.BattlesList[12].LastMissionCompleted;

            nudSecondaryObjectivesCompB1.Value = pilot.ListOfBattles.BattlesList[0].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB2.Value = pilot.ListOfBattles.BattlesList[1].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB3.Value = pilot.ListOfBattles.BattlesList[2].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB4.Value = pilot.ListOfBattles.BattlesList[3].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB5.Value = pilot.ListOfBattles.BattlesList[4].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB6.Value = pilot.ListOfBattles.BattlesList[5].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB7.Value = pilot.ListOfBattles.BattlesList[6].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB8.Value = pilot.ListOfBattles.BattlesList[7].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB9.Value = pilot.ListOfBattles.BattlesList[8].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB10.Value = pilot.ListOfBattles.BattlesList[9].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB11.Value = pilot.ListOfBattles.BattlesList[10].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB12.Value = pilot.ListOfBattles.BattlesList[11].SecondaryObjectivesCompleted;
            nudSecondaryObjectivesCompB13.Value = pilot.ListOfBattles.BattlesList[12].SecondaryObjectivesCompleted;

            nudBonusObjectivesCompB1.Value = pilot.ListOfBattles.BattlesList[0].BonusObjectivesCompleted;
            nudBonusObjectivesCompB2.Value = pilot.ListOfBattles.BattlesList[1].BonusObjectivesCompleted;
            nudBonusObjectivesCompB3.Value = pilot.ListOfBattles.BattlesList[2].BonusObjectivesCompleted;
            nudBonusObjectivesCompB4.Value = pilot.ListOfBattles.BattlesList[3].BonusObjectivesCompleted;
            nudBonusObjectivesCompB5.Value = pilot.ListOfBattles.BattlesList[4].BonusObjectivesCompleted;
            nudBonusObjectivesCompB6.Value = pilot.ListOfBattles.BattlesList[5].BonusObjectivesCompleted;
            nudBonusObjectivesCompB7.Value = pilot.ListOfBattles.BattlesList[6].BonusObjectivesCompleted;
            nudBonusObjectivesCompB8.Value = pilot.ListOfBattles.BattlesList[7].BonusObjectivesCompleted;
            nudBonusObjectivesCompB9.Value = pilot.ListOfBattles.BattlesList[8].BonusObjectivesCompleted;
            nudBonusObjectivesCompB10.Value = pilot.ListOfBattles.BattlesList[9].BonusObjectivesCompleted;
            nudBonusObjectivesCompB11.Value = pilot.ListOfBattles.BattlesList[10].BonusObjectivesCompleted;
            nudBonusObjectivesCompB12.Value = pilot.ListOfBattles.BattlesList[11].BonusObjectivesCompleted;
            nudBonusObjectivesCompB13.Value = pilot.ListOfBattles.BattlesList[12].BonusObjectivesCompleted;

            // #TODO Populate check boxes for objectives for all battles
            List<bool> battleSecondaryObjectives = new List<bool>();
            SecondaryObjectives secondaryObjectives = (SecondaryObjectives)pilot.ListOfBattles.BattlesList[0].SecondaryObjectivesCompleted;
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.First) == SecondaryObjectives.First);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Second) == SecondaryObjectives.Second);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Third) == SecondaryObjectives.Third);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Fourth) == SecondaryObjectives.Fourth);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Fifth) == SecondaryObjectives.Fifth);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Sixth) == SecondaryObjectives.Sixth);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Seventh) == SecondaryObjectives.Seventh);
            battleSecondaryObjectives.Add((secondaryObjectives & SecondaryObjectives.Eighth) == SecondaryObjectives.Eighth);

            for (int i = 0; i < clbSecondaryObjectiveB1.Items.Count - 1; i++)
            {
                if (battleSecondaryObjectives[i])
                {
                    clbSecondaryObjectiveB1.SetItemCheckState(i, CheckState.Checked);
                }
                else
                {
                    clbSecondaryObjectiveB1.SetItemCheckState(i, CheckState.Unchecked);
                }
            }

            lblTotalScoreB1.Text = pilot.ListOfBattles.BattlesList[0].TotalScore.ToString();
            lblTotalScoreB2.Text = pilot.ListOfBattles.BattlesList[1].TotalScore.ToString();
            lblTotalScoreB3.Text = pilot.ListOfBattles.BattlesList[2].TotalScore.ToString();
            lblTotalScoreB4.Text = pilot.ListOfBattles.BattlesList[3].TotalScore.ToString();
            lblTotalScoreB5.Text = pilot.ListOfBattles.BattlesList[4].TotalScore.ToString();
            lblTotalScoreB6.Text = pilot.ListOfBattles.BattlesList[5].TotalScore.ToString();
            lblTotalScoreB7.Text = pilot.ListOfBattles.BattlesList[6].TotalScore.ToString();
            lblTotalScoreB8.Text = pilot.ListOfBattles.BattlesList[7].TotalScore.ToString();
            lblTotalScoreB9.Text = pilot.ListOfBattles.BattlesList[8].TotalScore.ToString();
            lblTotalScoreB10.Text = pilot.ListOfBattles.BattlesList[9].TotalScore.ToString();
            lblTotalScoreB11.Text = pilot.ListOfBattles.BattlesList[10].TotalScore.ToString();
            lblTotalScoreB12.Text = pilot.ListOfBattles.BattlesList[11].TotalScore.ToString();
            lblTotalScoreB13.Text = pilot.ListOfBattles.BattlesList[12].TotalScore.ToString();

            nudLaserHits.Value = pilot.BattleStats.LaserCraftHits;
            nudLaserFired.Value = pilot.BattleStats.LasersFired;
            nudWarheadHits.Value = pilot.BattleStats.WarheadHits;
            nudWarheadFired.Value = pilot.BattleStats.WarheadsFired;
            nudTotalKills.Value = pilot.BattleStats.TotalKills;
            nudTotalCaptures.Value = pilot.BattleStats.TotalCaptures;
            nudCraftLost.Value = pilot.BattleStats.CraftLost;

            if (nudLaserFired.Value != 0)
            {
                lblLaserPercent.Text = "(" + (int)((nudLaserHits.Value / nudLaserFired.Value) * 100) + "%)";
            }
            else
            {
                lblLaserPercent.Text = "(0%)";
            }

            if (nudWarheadFired.Value != 0)
            {
                lblWarheadPercent.Text = "(" + (int)((nudWarheadHits.Value / nudWarheadFired.Value) * 100) + "%)";
            }
            else
            {
                lblWarheadPercent.Text = "(0%)";
            }

            nudHistCombatPointsTFM1.Value = pilot.HistoricCombatRecordList[0].MissionScore[0];
            nudHistCombatPointsTFM2.Value = pilot.HistoricCombatRecordList[0].MissionScore[1];
            nudHistCombatPointsTFM3.Value = pilot.HistoricCombatRecordList[0].MissionScore[2];
            nudHistCombatPointsTFM4.Value = pilot.HistoricCombatRecordList[0].MissionScore[3];
            cbHistCombatCompTFM1.Checked = pilot.HistoricCombatRecordList[0].MissionComplete[0];
            cbHistCombatCompTFM2.Checked = pilot.HistoricCombatRecordList[0].MissionComplete[1];
            cbHistCombatCompTFM3.Checked = pilot.HistoricCombatRecordList[0].MissionComplete[2];
            cbHistCombatCompTFM4.Checked = pilot.HistoricCombatRecordList[0].MissionComplete[3];

            nudHistCombatPointsTIM1.Value = pilot.HistoricCombatRecordList[1].MissionScore[0];
            nudHistCombatPointsTIM2.Value = pilot.HistoricCombatRecordList[1].MissionScore[1];
            nudHistCombatPointsTIM3.Value = pilot.HistoricCombatRecordList[1].MissionScore[2];
            nudHistCombatPointsTIM4.Value = pilot.HistoricCombatRecordList[1].MissionScore[3];
            cbHistCombatCompTIM1.Checked = pilot.HistoricCombatRecordList[1].MissionComplete[0];
            cbHistCombatCompTIM2.Checked = pilot.HistoricCombatRecordList[1].MissionComplete[1];
            cbHistCombatCompTIM3.Checked = pilot.HistoricCombatRecordList[1].MissionComplete[2];
            cbHistCombatCompTIM4.Checked = pilot.HistoricCombatRecordList[1].MissionComplete[3];

            nudHistCombatPointsTBM1.Value = pilot.HistoricCombatRecordList[2].MissionScore[0];
            nudHistCombatPointsTBM2.Value = pilot.HistoricCombatRecordList[2].MissionScore[1];
            nudHistCombatPointsTBM3.Value = pilot.HistoricCombatRecordList[2].MissionScore[2];
            nudHistCombatPointsTBM4.Value = pilot.HistoricCombatRecordList[2].MissionScore[3];
            cbHistCombatCompTBM1.Checked = pilot.HistoricCombatRecordList[2].MissionComplete[0];
            cbHistCombatCompTBM2.Checked = pilot.HistoricCombatRecordList[2].MissionComplete[1];
            cbHistCombatCompTBM3.Checked = pilot.HistoricCombatRecordList[2].MissionComplete[2];
            cbHistCombatCompTBM4.Checked = pilot.HistoricCombatRecordList[2].MissionComplete[3];

            nudHistCombatPointsTAM1.Value = pilot.HistoricCombatRecordList[3].MissionScore[0];
            nudHistCombatPointsTAM2.Value = pilot.HistoricCombatRecordList[3].MissionScore[1];
            nudHistCombatPointsTAM3.Value = pilot.HistoricCombatRecordList[3].MissionScore[2];
            nudHistCombatPointsTAM4.Value = pilot.HistoricCombatRecordList[3].MissionScore[3];
            cbHistCombatCompTAM1.Checked = pilot.HistoricCombatRecordList[3].MissionComplete[0];
            cbHistCombatCompTAM2.Checked = pilot.HistoricCombatRecordList[3].MissionComplete[1];
            cbHistCombatCompTAM3.Checked = pilot.HistoricCombatRecordList[3].MissionComplete[2];
            cbHistCombatCompTAM4.Checked = pilot.HistoricCombatRecordList[3].MissionComplete[3];

            nudHistCombatPointsAGM1.Value = pilot.HistoricCombatRecordList[4].MissionScore[0];
            nudHistCombatPointsAGM2.Value = pilot.HistoricCombatRecordList[4].MissionScore[1];
            nudHistCombatPointsAGM3.Value = pilot.HistoricCombatRecordList[4].MissionScore[2];
            nudHistCombatPointsAGM4.Value = pilot.HistoricCombatRecordList[4].MissionScore[3];
            cbHistCombatCompAGM1.Checked = pilot.HistoricCombatRecordList[4].MissionComplete[0];
            cbHistCombatCompAGM2.Checked = pilot.HistoricCombatRecordList[4].MissionComplete[1];
            cbHistCombatCompAGM3.Checked = pilot.HistoricCombatRecordList[4].MissionComplete[2];
            cbHistCombatCompAGM4.Checked = pilot.HistoricCombatRecordList[4].MissionComplete[3];

            nudHistCombatPointsTDM1.Value = pilot.HistoricCombatRecordList[5].MissionScore[0];
            nudHistCombatPointsTDM2.Value = pilot.HistoricCombatRecordList[5].MissionScore[1];
            nudHistCombatPointsTDM3.Value = pilot.HistoricCombatRecordList[5].MissionScore[2];
            nudHistCombatPointsTDM4.Value = pilot.HistoricCombatRecordList[5].MissionScore[3];
            cbHistCombatCompTDM1.Checked = pilot.HistoricCombatRecordList[5].MissionComplete[0];
            cbHistCombatCompTDM2.Checked = pilot.HistoricCombatRecordList[5].MissionComplete[1];
            cbHistCombatCompTDM3.Checked = pilot.HistoricCombatRecordList[5].MissionComplete[2];
            cbHistCombatCompTDM4.Checked = pilot.HistoricCombatRecordList[5].MissionComplete[3];

            nudHistCombatPointsMBM1.Value = pilot.HistoricCombatRecordList[6].MissionScore[0];
            nudHistCombatPointsMBM2.Value = pilot.HistoricCombatRecordList[6].MissionScore[1];
            nudHistCombatPointsMBM3.Value = pilot.HistoricCombatRecordList[6].MissionScore[2];
            nudHistCombatPointsMBM4.Value = pilot.HistoricCombatRecordList[6].MissionScore[3];
            cbHistCombatCompMBM1.Checked = pilot.HistoricCombatRecordList[6].MissionComplete[0];
            cbHistCombatCompMBM2.Checked = pilot.HistoricCombatRecordList[6].MissionComplete[1];
            cbHistCombatCompMBM3.Checked = pilot.HistoricCombatRecordList[6].MissionComplete[2];
            cbHistCombatCompMBM4.Checked = pilot.HistoricCombatRecordList[6].MissionComplete[3];

            this.Refresh();
        }

        //Form Control Change Methods

        //--------------------------------------------------------------------
        //Pilot Status Tab

        private void comHealth_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.PltHealth = (Health)comHealth.SelectedItem;
            UpdateForm();
        }

        private void comRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.PltRank = (Rank)comRank.SelectedItem;
            UpdateForm();
        }

        private void comDifficulty_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.PltDifficulty = (Difficulty)comDifficulty.SelectedItem;
            UpdateForm();
        }

        private void nudBattleScore_ValueChanged(object sender, EventArgs e)
        {
            pilot.PltBattleScore = (int)nudBattleScore.Value;
        }

        private void nudSkill_ValueChanged(object sender, EventArgs e)
        {
            pilot.PltSkillLevel = (uint)nudSkill.Value;
        }

        private void comSecretOrderRank_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.PltSecretOrderLevel = (SecretOrder)comSecretOrderRank.SelectedItem;
            UpdateForm();
        }

        private void nudBattleScore_Enter(object sender, EventArgs e)
        {
            nudBattleScore.Select(0, nudBattleScore.Text.Length);
        }

        private void nudSkill_Enter(object sender, EventArgs e)
        {
            nudSkill.Select(0, nudSkill.Text.Length);
        }

        //--------------------------------------------------------------------
        //TIE Fighter

        //These update the mission score when the value is changed
        private void nudHistCombatPointsTFM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionScore[0] = (uint)nudHistCombatPointsTFM1.Value;
        }

        private void nudHistCombatPointsTFM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionScore[1] = (uint)nudHistCombatPointsTFM2.Value;
        }

        private void nudHistCombatPointsTFM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionScore[2] = (uint)nudHistCombatPointsTFM3.Value;
        }

        private void nudHistCombatPointsTFM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionScore[3] = (uint)nudHistCombatPointsTFM4.Value;
        }

        //These auto-select the value in the control when focus changes to the control
        private void nudHistCombatPointsTFM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTFM1.Select(0, nudHistCombatPointsTFM1.Text.Length);
        }

        private void nudHistCombatPointsTFM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTFM2.Select(0, nudHistCombatPointsTFM2.Text.Length);
        }

        private void nudHistCombatPointsTFM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTFM3.Select(0, nudHistCombatPointsTFM3.Text.Length);
        }

        private void nudHistCombatPointsTFM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTFM4.Select(0, nudHistCombatPointsTFM4.Text.Length);
        }

        //These update the pilot data when the check box is changed
        private void cbHistCombatCompTFM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionComplete[0] = cbHistCombatCompTFM1.Checked;
        }

        private void cbHistCombatCompTFM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionComplete[1] = cbHistCombatCompTFM1.Checked;
        }

        private void cbHistCombatCompTFM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionComplete[2] = cbHistCombatCompTFM1.Checked;
        }

        private void cbHistCombatCompTFM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[0].MissionComplete[3] = cbHistCombatCompTFM1.Checked;
        }

        //--------------------------------------------------------------------
        //TIE Interceptor

        private void nudHistCombatPointsTIM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionScore[0] = (uint)nudHistCombatPointsTIM1.Value;
        }

        private void nudHistCombatPointsTIM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionScore[1] = (uint)nudHistCombatPointsTIM2.Value;
        }

        private void nudHistCombatPointsTIM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionScore[2] = (uint)nudHistCombatPointsTIM3.Value;
        }

        private void nudHistCombatPointsTIM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionScore[3] = (uint)nudHistCombatPointsTIM4.Value;
        }

        private void nudHistCombatPointsTIM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTIM1.Select(0, nudHistCombatPointsTIM1.Text.Length);
        }

        private void nudHistCombatPointsTIM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTIM2.Select(0, nudHistCombatPointsTIM2.Text.Length);
        }

        private void nudHistCombatPointsTIM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTIM3.Select(0, nudHistCombatPointsTIM3.Text.Length);
        }

        private void nudHistCombatPointsTIM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTIM4.Select(0, nudHistCombatPointsTIM4.Text.Length);
        }

        private void cbHistCombatCompTIM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionComplete[0] = cbHistCombatCompTIM1.Checked;
        }

        private void cbHistCombatCompTIM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionComplete[1] = cbHistCombatCompTIM2.Checked;
        }

        private void cbHistCombatCompTIM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionComplete[2] = cbHistCombatCompTIM3.Checked;
        }

        private void cbHistCombatCompTIM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[1].MissionComplete[3] = cbHistCombatCompTIM4.Checked;
        }

        //--------------------------------------------------------------------
        //TIE Bomber

        private void nudHistCombatPointsTBM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionScore[0] = (uint)nudHistCombatPointsTBM1.Value;
        }

        private void nudHistCombatPointsTBM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionScore[1] = (uint)nudHistCombatPointsTBM2.Value;
        }

        private void nudHistCombatPointsTBM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionScore[2] = (uint)nudHistCombatPointsTBM3.Value;
        }

        private void nudHistCombatPointsTBM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionScore[3] = (uint)nudHistCombatPointsTBM4.Value;
        }

        private void nudHistCombatPointsTBM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTBM1.Select(0, nudHistCombatPointsTBM1.Text.Length);
        }

        private void nudHistCombatPointsTBM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTBM2.Select(0, nudHistCombatPointsTBM2.Text.Length);
        }

        private void nudHistCombatPointsTBM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTBM3.Select(0, nudHistCombatPointsTBM3.Text.Length);
        }

        private void nudHistCombatPointsTBM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTBM4.Select(0, nudHistCombatPointsTBM4.Text.Length);
        }

        private void cbHistCombatCompTBM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionComplete[0] = cbHistCombatCompTBM1.Checked;
        }

        private void cbHistCombatCompTBM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionComplete[1] = cbHistCombatCompTBM2.Checked;
        }

        private void cbHistCombatCompTBM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionComplete[2] = cbHistCombatCompTBM3.Checked;
        }

        private void cbHistCombatCompTBM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[2].MissionComplete[3] = cbHistCombatCompTBM4.Checked;
        }

        //--------------------------------------------------------------------
        //TIE Advanced

        private void nudHistCombatPointsTAM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionScore[0] = (uint)nudHistCombatPointsTAM1.Value;
        }

        private void nudHistCombatPointsTAM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionScore[1] = (uint)nudHistCombatPointsTAM2.Value;
        }

        private void nudHistCombatPointsTAM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionScore[2] = (uint)nudHistCombatPointsTAM3.Value;
        }

        private void nudHistCombatPointsTAM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionScore[3] = (uint)nudHistCombatPointsTAM4.Value;
        }

        private void nudHistCombatPointsTAM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTAM1.Select(0, nudHistCombatPointsTAM1.Text.Length);
        }

        private void nudHistCombatPointsTAM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTAM2.Select(0, nudHistCombatPointsTAM2.Text.Length);
        }

        private void nudHistCombatPointsTAM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTAM3.Select(0, nudHistCombatPointsTAM3.Text.Length);
        }

        private void nudHistCombatPointsTAM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTAM4.Select(0, nudHistCombatPointsTAM4.Text.Length);
        }

        private void cbHistCombatCompTAM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionComplete[0] = cbHistCombatCompTAM1.Checked;
        }

        private void cbHistCombatCompTAM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionComplete[1] = cbHistCombatCompTAM2.Checked;
        }

        private void cbHistCombatCompTAM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionComplete[2] = cbHistCombatCompTAM3.Checked;
        }

        private void cbHistCombatCompTAM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[3].MissionComplete[3] = cbHistCombatCompTAM4.Checked;
        }

        //--------------------------------------------------------------------
        //Assault Gunboat

        private void nudHistCombatPointsAGM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionScore[0] = (uint)nudHistCombatPointsAGM1.Value;
        }

        private void nudHistCombatPointsAGM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionScore[1] = (uint)nudHistCombatPointsAGM2.Value;
        }

        private void nudHistCombatPointsAGM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionScore[2] = (uint)nudHistCombatPointsAGM3.Value;
        }

        private void nudHistCombatPointsAGM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionScore[3] = (uint)nudHistCombatPointsAGM4.Value;
        }

        private void nudHistCombatPointsAGM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsAGM1.Select(0, nudHistCombatPointsAGM1.Text.Length);
        }

        private void nudHistCombatPointsAGM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsAGM2.Select(0, nudHistCombatPointsAGM2.Text.Length);
        }

        private void nudHistCombatPointsAGM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsAGM3.Select(0, nudHistCombatPointsAGM3.Text.Length);
        }

        private void nudHistCombatPointsAGM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsAGM4.Select(0, nudHistCombatPointsAGM4.Text.Length);
        }

        private void cbHistCombatCompAGM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionComplete[0] = cbHistCombatCompAGM1.Checked;
        }

        private void cbHistCombatCompAGM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionComplete[1] = cbHistCombatCompAGM2.Checked;
        }

        private void cbHistCombatCompAGM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionComplete[2] = cbHistCombatCompAGM3.Checked;
        }

        private void cbHistCombatCompAGM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[4].MissionComplete[3] = cbHistCombatCompAGM4.Checked;
        }

        //--------------------------------------------------------------------
        //TIE Defender

        private void nudHistCombatPointsTDM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionScore[0] = (uint)nudHistCombatPointsTDM1.Value;
        }

        private void nudHistCombatPointsTDM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionScore[1] = (uint)nudHistCombatPointsTDM2.Value;
        }

        private void nudHistCombatPointsTDM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionScore[2] = (uint)nudHistCombatPointsTDM3.Value;
        }

        private void nudHistCombatPointsTDM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionScore[3] = (uint)nudHistCombatPointsTDM4.Value;
        }

        private void nudHistCombatPointsTDM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTDM1.Select(0, nudHistCombatPointsTDM1.Text.Length);
        }

        private void nudHistCombatPointsTDM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTDM2.Select(0, nudHistCombatPointsTDM2.Text.Length);
        }

        private void nudHistCombatPointsTDM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTDM3.Select(0, nudHistCombatPointsTDM3.Text.Length);
        }

        private void nudHistCombatPointsTDM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsTDM4.Select(0, nudHistCombatPointsTDM4.Text.Length);
        }

        private void cbHistCombatCompTDM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionComplete[0] = cbHistCombatCompTDM1.Checked;
        }

        private void cbHistCombatCompTDM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionComplete[1] = cbHistCombatCompTDM2.Checked;
        }

        private void cbHistCombatCompTDM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionComplete[2] = cbHistCombatCompTDM3.Checked;
        }

        private void cbHistCombatCompTDM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[5].MissionComplete[3] = cbHistCombatCompTDM4.Checked;
        }

        //--------------------------------------------------------------------
        //Missile Boat

        private void nudHistCombatPointsMBM1_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionScore[0] = (uint)nudHistCombatPointsMBM1.Value;
        }

        private void nudHistCombatPointsMBM2_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionScore[1] = (uint)nudHistCombatPointsMBM2.Value;
        }

        private void nudHistCombatPointsMBM3_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionScore[2] = (uint)nudHistCombatPointsMBM3.Value;
        }

        private void nudHistCombatPointsMBM4_ValueChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionScore[3] = (uint)nudHistCombatPointsMBM4.Value;
        }

        private void nudHistCombatPointsMBM1_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsMBM1.Select(0, nudHistCombatPointsMBM1.Text.Length);
        }

        private void nudHistCombatPointsMBM2_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsMBM2.Select(0, nudHistCombatPointsMBM2.Text.Length);
        }

        private void nudHistCombatPointsMBM3_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsMBM3.Select(0, nudHistCombatPointsMBM3.Text.Length);
        }

        private void nudHistCombatPointsMBM4_Enter(object sender, EventArgs e)
        {
            nudHistCombatPointsMBM4.Select(0, nudHistCombatPointsMBM4.Text.Length);
        }

        private void cbHistCombatCompMBM1_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionComplete[0] = cbHistCombatCompMBM1.Checked;
        }

        private void cbHistCombatCompMBM2_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionComplete[1] = cbHistCombatCompMBM2.Checked;
        }

        private void cbHistCombatCompMBM3_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionComplete[2] = cbHistCombatCompMBM3.Checked;
        }

        private void cbHistCombatCompMBM4_CheckedChanged(object sender, EventArgs e)
        {
            pilot.HistoricCombatRecordList[6].MissionComplete[3] = cbHistCombatCompMBM4.Checked;
        }

        //--------------------------------------------------------------------
        //Battle Stats

        private void nudLaserHits_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.LaserCraftHits = (uint)nudLaserHits.Value;
            UpdateForm();
        }

        private void nudLaserFired_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.LasersFired = (uint)nudLaserFired.Value;
            UpdateForm();
        }

        private void nudWarheadHits_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.WarheadHits = (ushort)nudWarheadHits.Value;
            UpdateForm();
        }

        private void nudWarheadFired_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.WarheadsFired = (ushort)nudWarheadFired.Value;
            UpdateForm();
        }

        private void nudTotalKills_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.TotalKills = (ushort)nudTotalKills.Value;
        }

        private void nudTotalCaptures_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.TotalCaptures = (ushort)nudTotalCaptures.Value;
        }

        private void nudCraftLost_ValueChanged(object sender, EventArgs e)
        {
            pilot.BattleStats.CraftLost = (ushort)nudCraftLost.Value;
        }

        private void nudLaserHits_Enter(object sender, EventArgs e)
        {
            nudLaserHits.Select(0, nudLaserHits.Text.Length);
        }

        private void nudLaserFired_Enter(object sender, EventArgs e)
        {
            nudLaserFired.Select(0, nudLaserFired.Text.Length);
        }

        private void nudWarheadHits_Enter(object sender, EventArgs e)
        {
            nudWarheadHits.Select(0, nudWarheadHits.Text.Length);
        }

        private void nudWarheadFired_Enter(object sender, EventArgs e)
        {
            nudWarheadFired.Select(0, nudWarheadFired.Text.Length);
        }

        private void nudTotalKills_Enter(object sender, EventArgs e)
        {
            nudTotalKills.Select(0, nudTotalKills.Text.Length);
        }

        private void nudTotalCaptures_Enter(object sender, EventArgs e)
        {
            nudTotalCaptures.Select(0, nudTotalCaptures.Text.Length);
        }

        private void nudCraftLost_Enter(object sender, EventArgs e)
        {
            nudCraftLost.Select(0, nudCraftLost.Text.Length);
        }

        //--------------------------------------------------------------------
        //Battle Status

        //--------------------------------------------------------------------
        //Battle 1

        private void comStatusB1_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[0].Status = (BattleStatus)comStatusB1.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB1_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[0].LastMissionCompleted = (int)nudLastMissionCompB1.Value;
        }

        private void nudSecondaryObjCompB1_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[0].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB1.Value;
        }

        private void nudBonusObjCompB1_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[0].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB1.Value;
        }

        private void nudLastMissionCompB1_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB1.Select(0, nudLastMissionCompB1.Text.Length);
        }

        private void nudSecondaryObjCompB1_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB1.Select(0, nudSecondaryObjectivesCompB1.Text.Length);
        }

        private void nudBonusObjCompB1_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB1.Select(0, nudBonusObjectivesCompB1.Text.Length);
        }

        private void gvBattle1Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[0].UpdateBattleTotalScore(6);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 2

        private void comStatusB2_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[1].Status = (BattleStatus)comStatusB2.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB2_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[1].LastMissionCompleted = (int)nudLastMissionCompB2.Value;
        }

        private void nudSecondaryObjectivesCompB2_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[1].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB2.Value;
        }

        private void nudBonusObjectivesCompB2_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[1].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB2.Value;
        }

        private void nudLastMissionCompB2_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB2.Select(0, nudLastMissionCompB2.Text.Length);
        }

        private void nudSecondaryObjectivesCompB2_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB2.Select(0, nudSecondaryObjectivesCompB2.Text.Length);
        }

        private void nudBonusObjectivesCompB2_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB2.Select(0, nudBonusObjectivesCompB2.Text.Length);
        }

        private void gvBattle2Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[1].UpdateBattleTotalScore(5);
            UpdateForm();
        }


        //--------------------------------------------------------------------
        //Battle 3

        private void comStatusB3_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[2].Status = (BattleStatus)comStatusB3.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB3_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[2].LastMissionCompleted = (int)nudLastMissionCompB3.Value;
        }

        private void nudSecondaryObjectivesCompB3_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[2].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB3.Value;
        }

        private void nudBonusObjectivesCompB3_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[2].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB3.Value;
        }

        private void nudLastMissionCompB3_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB3.Select(0, nudLastMissionCompB3.Text.Length);
        }

        private void nudSecondaryObjectivesCompB3_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB3.Select(0, nudSecondaryObjectivesCompB3.Text.Length);
        }

        private void nudBonusObjectivesCompB3_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB3.Select(0, nudBonusObjectivesCompB3.Text.Length);
        }

        private void gvBattle3Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[2].UpdateBattleTotalScore(6);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 4

        private void comStatusB4_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[3].Status = (BattleStatus)comStatusB4.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB4_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[3].LastMissionCompleted = (int)nudLastMissionCompB4.Value;
        }

        private void nudSecondaryObjectivesCompB4_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[3].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB4.Value;
        }

        private void nudBonusObjectivesCompB4_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[3].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB4.Value;
        }

        private void nudLastMissionCompB4_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB4.Select(0, nudLastMissionCompB4.Text.Length);
        }

        private void nudSecondaryObjectivesCompB4_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB4.Select(0, nudSecondaryObjectivesCompB4.Text.Length);
        }

        private void nudBonusObjectivesCompB4_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB4.Select(0, nudBonusObjectivesCompB4.Text.Length);
        }

        private void gvBattle4Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[3].UpdateBattleTotalScore(5);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 5

        private void comStatusB5_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[4].Status = (BattleStatus)comStatusB5.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB5_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[4].LastMissionCompleted = (int)nudLastMissionCompB5.Value;
        }

        private void nudSecondaryObjectivesCompB5_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[4].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB5.Value;
        }

        private void nudBonusObjectivesCompB5_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[4].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB5.Value;
        }

        private void nudLastMissionCompB5_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB5.Select(0, nudLastMissionCompB5.Text.Length);
        }

        private void nudSecondaryObjectivesCompB5_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB5.Select(0, nudSecondaryObjectivesCompB5.Text.Length);
        }

        private void nudBonusObjectivesCompB5_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB5.Select(0, nudBonusObjectivesCompB5.Text.Length);
        }

        private void gvBattle5Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[4].UpdateBattleTotalScore(5);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 6

        private void comStatusB6_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[5].Status = (BattleStatus)comStatusB6.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB6_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[5].LastMissionCompleted = (int)nudLastMissionCompB6.Value;
        }

        private void nudSecondaryObjectivesCompB6_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[5].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB6.Value;
        }

        private void nudBonusObjectivesCompB6_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[5].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB6.Value;
        }

        private void nudLastMissionCompB6_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB6.Select(0, nudLastMissionCompB6.Text.Length);
        }

        private void nudSecondaryObjectivesCompB6_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB6.Select(0, nudSecondaryObjectivesCompB6.Text.Length);
        }

        private void nudBonusObjectivesCompB6_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB6.Select(0, nudBonusObjectivesCompB6.Text.Length);
        }

        private void gvBattle6Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[5].UpdateBattleTotalScore(4);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 7

        private void comStatusB7_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[6].Status = (BattleStatus)comStatusB7.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB7_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[6].LastMissionCompleted = (int)nudLastMissionCompB7.Value;
        }

        private void nudSecondaryObjectivesCompB7_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[6].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB7.Value;
        }

        private void nudBonusObjectivesCompB7_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[6].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB7.Value;
        }

        private void nudLastMissionCompB7_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB7.Select(0, nudLastMissionCompB7.Text.Length);
        }

        private void nudSecondaryObjectivesCompB7_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB7.Select(0, nudSecondaryObjectivesCompB7.Text.Length);
        }

        private void nudBonusObjectivesCompB7_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB7.Select(0, nudBonusObjectivesCompB7.Text.Length);
        }

        private void gvBattle7Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[6].UpdateBattleTotalScore(5);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 8

        private void comStatusB8_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[7].Status = (BattleStatus)comStatusB8.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB8_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[7].LastMissionCompleted = (int)nudLastMissionCompB8.Value;
        }

        private void nudSecondaryObjectivesCompB8_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[7].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB8.Value;
        }

        private void nudBonusObjectivesCompB8_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[7].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB8.Value;
        }

        private void nudLastMissionCompB8_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB8.Select(0, nudLastMissionCompB8.Text.Length);
        }

        private void nudSecondaryObjectivesCompB8_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB8.Select(0, nudSecondaryObjectivesCompB8.Text.Length);
        }

        private void nudBonusObjectivesCompB8_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB8.Select(0, nudBonusObjectivesCompB8.Text.Length);
        }

        private void gvBattle8Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[7].UpdateBattleTotalScore(6);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 9

        private void comStatusB9_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[8].Status = (BattleStatus)comStatusB9.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB9_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[8].LastMissionCompleted = (int)nudLastMissionCompB9.Value;
        }

        private void nudSecondaryObjectivesCompB9_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[8].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB9.Value;
        }

        private void nudBonusObjectivesCompB9_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[8].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB9.Value;
        }

        private void nudLastMissionCompB9_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB9.Select(0, nudLastMissionCompB9.Text.Length);
        }

        private void nudSecondaryObjectivesCompB9_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB9.Select(0, nudSecondaryObjectivesCompB9.Text.Length);
        }

        private void nudBonusObjectivesCompB9_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB9.Select(0, nudBonusObjectivesCompB9.Text.Length);
        }

        private void gvBattle9Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[8].UpdateBattleTotalScore(6);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 10

        private void comStatusB10_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[9].Status = (BattleStatus)comStatusB10.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB10_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[9].LastMissionCompleted = (int)nudLastMissionCompB10.Value;
        }

        private void nudSecondaryObjectivesCompB10_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[9].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB10.Value;
        }

        private void nudBonusObjectivesCompB10_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[9].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB10.Value;
        }

        private void nudLastMissionCompB10_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB10.Select(0, nudLastMissionCompB10.Text.Length);
        }

        private void nudSecondaryObjectivesCompB10_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB10.Select(0, nudSecondaryObjectivesCompB10.Text.Length);
        }

        private void nudBonusObjectivesCompB10_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB10.Select(0, nudBonusObjectivesCompB10.Text.Length);
        }

        private void gvBattle10Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[9].UpdateBattleTotalScore(6);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 11

        private void comStatusB11_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[10].Status = (BattleStatus)comStatusB11.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB11_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[10].LastMissionCompleted = (int)nudLastMissionCompB11.Value;
        }

        private void nudSecondaryObjectivesCompB11_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[10].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB11.Value;
        }

        private void nudBonusObjectivesCompB11_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[10].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB11.Value;
        }

        private void nudLastMissionCompB11_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB11.Select(0, nudLastMissionCompB11.Text.Length);
        }

        private void nudSecondaryObjectivesCompB11_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB11.Select(0, nudSecondaryObjectivesCompB11.Text.Length);
        }

        private void nudBonusObjectivesCompB11_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB11.Select(0, nudBonusObjectivesCompB11.Text.Length);
        }

        private void gvBattle11Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[10].UpdateBattleTotalScore(7);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 12

        private void comStatusB12_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[11].Status = (BattleStatus)comStatusB12.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB12_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[11].LastMissionCompleted = (int)nudLastMissionCompB12.Value;
        }

        private void nudSecondaryObjectivesCompB12_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[11].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB12.Value;
        }

        private void nudBonusObjectivesCompB12_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[11].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB12.Value;
        }

        private void nudLastMissionCompB12_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB12.Select(0, nudLastMissionCompB12.Text.Length);
        }

        private void nudSecondaryObjectivesCompB12_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB12.Select(0, nudSecondaryObjectivesCompB12.Text.Length);
        }

        private void nudBonusObjectivesCompB12_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB12.Select(0, nudBonusObjectivesCompB12.Text.Length);
        }

        private void gvBattle12Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[11].UpdateBattleTotalScore(7);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        //Battle 13

        private void comStatusB13_SelectedIndexChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[12].Status = (BattleStatus)comStatusB13.SelectedItem;
            UpdateForm();
        }

        private void nudLastMissionCompB13_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[12].LastMissionCompleted = (int)nudLastMissionCompB13.Value;
        }

        private void nudSecondaryObjectivesCompB13_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[12].SecondaryObjectivesCompleted = (byte)nudSecondaryObjectivesCompB13.Value;
        }

        private void nudBonusObjectivesCompB13_ValueChanged(object sender, EventArgs e)
        {
            pilot.ListOfBattles.BattlesList[12].BonusObjectivesCompleted = (byte)nudBonusObjectivesCompB13.Value;
        }

        private void nudLastMissionCompB13_Enter(object sender, EventArgs e)
        {
            nudLastMissionCompB13.Select(0, nudLastMissionCompB13.Text.Length);
        }

        private void nudSecondaryObjectivesCompB13_Enter(object sender, EventArgs e)
        {
            nudSecondaryObjectivesCompB13.Select(0, nudSecondaryObjectivesCompB13.Text.Length);
        }

        private void nudBonusObjectivesCompB13_Enter(object sender, EventArgs e)
        {
            nudBonusObjectivesCompB13.Select(0, nudBonusObjectivesCompB13.Text.Length);
        }

        private void gvBattle13Scores_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            pilot.ListOfBattles.BattlesList[12].UpdateBattleTotalScore(8);
            UpdateForm();
        }

        //--------------------------------------------------------------------
        // Validate data entry in gvTraining

        private void gvTraining_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvTraining.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvTrainingMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvTraining.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                        gvTraining.CurrentCell.Value = 0;
                    }
                }
            }
        }

        private void gvTraining_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvTraining.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        //--------------------------------------------------------------------
        // Validate data entry in gvBattleVictories

        private void gvBattleVictories_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattleVictories.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleVictoriesMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattleVictories.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattleVictories_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattleVictories.Rows[e.RowIndex].ErrorText = String.Empty;
        }

        //--------------------------------------------------------------------
        // Validate data entry in gvBattleXScores

        private void gvBattle1Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle1Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle1Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle1Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle1Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle2Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle2Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle2Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle2Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle2Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle3Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle3Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle3Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle3Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle3Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle4Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle4Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle4Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle4Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle4Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle5Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle5Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle5Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle5Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle5Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle6Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle6Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle6Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle6Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle6Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle7Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle7Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle7Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle7Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle7Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle8Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle8Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle8Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle8Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle8Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle9Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle9Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle9Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle9Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle9Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle10Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle10Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle10Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle10Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle10Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle11Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle11Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle11Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle11Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle11Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle12Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle12Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle12Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle12Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle12Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        private void gvBattle13Scores_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            string headerText = gvBattle13Scores.Columns[e.ColumnIndex].HeaderText;

            if (!string.IsNullOrEmpty(headerText))
            {
                int maxValue;
                if (gvBattleScoresMaxValueByHeaderTextDict.TryGetValue(headerText, out maxValue))
                {
                    int newInteger;
                    if (!int.TryParse(e.FormattedValue.ToString(), out newInteger) || newInteger < 0 || newInteger > maxValue)
                    {
                        gvBattle13Scores.Rows[e.RowIndex].ErrorText = string.Format("Please enter a numeric value between 0 and {0}. Press Esc to clear.", maxValue);
                        if (validationPopUp)
                        {
                            MessageBox.Show("Please enter a numeric value between 0 and " + maxValue + ". After clicking on OK, Press Esc to clear the cell.");
                        }

                        e.Cancel = true;
                    }
                }
            }
        }

        private void gvBattle13Scores_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gvBattle13Scores.Rows[e.RowIndex].ErrorText = string.Empty;
        }

        //Validation Pop-up Toggle

        private void cbValidationToggle_CheckedChanged(object sender, EventArgs e)
        {
            validationPopUp = cbValidationToggle.Checked;
        }
    }
}
