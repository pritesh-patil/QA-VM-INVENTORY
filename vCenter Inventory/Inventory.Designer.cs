namespace vCenter_Inventory
{
    partial class Inventory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Inventory));
            this.lbHost = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.listVM = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonPowerOn = new System.Windows.Forms.Button();
            this.buttonPowerOff = new System.Windows.Forms.Button();
            this.listBoxProductsInstalled = new System.Windows.Forms.ListBox();
            this.labelInstalledProducts = new System.Windows.Forms.Label();
            this.labelPowerOff = new System.Windows.Forms.Label();
            this.listBoxPowerOff = new System.Windows.Forms.ListBox();
            this.textBoxSearch = new System.Windows.Forms.TextBox();
            this.labelSearch = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.Progresslabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbHost
            // 
            this.lbHost.BackColor = System.Drawing.SystemColors.Menu;
            this.lbHost.FormattingEnabled = true;
            this.lbHost.Location = new System.Drawing.Point(248, 64);
            this.lbHost.Name = "lbHost";
            this.lbHost.Size = new System.Drawing.Size(446, 43);
            this.lbHost.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(16, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "VM List - Powered ON";
            // 
            // btnConnect
            // 
            this.btnConnect.BackColor = System.Drawing.Color.SteelBlue;
            this.btnConnect.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.btnConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.SystemColors.Control;
            this.btnConnect.Location = new System.Drawing.Point(12, 12);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(95, 23);
            this.btnConnect.TabIndex = 12;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = false;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // listVM
            // 
            this.listVM.BackColor = System.Drawing.SystemColors.Menu;
            this.listVM.FormattingEnabled = true;
            this.listVM.Location = new System.Drawing.Point(15, 64);
            this.listVM.Name = "listVM";
            this.listVM.Size = new System.Drawing.Size(214, 186);
            this.listVM.TabIndex = 13;
            this.listVM.SelectedIndexChanged += new System.EventHandler(this.listVM_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(245, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(110, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "VM Name Description";
            // 
            // buttonPowerOn
            // 
            this.buttonPowerOn.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonPowerOn.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.buttonPowerOn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPowerOn.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPowerOn.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonPowerOn.Location = new System.Drawing.Point(113, 12);
            this.buttonPowerOn.Name = "buttonPowerOn";
            this.buttonPowerOn.Size = new System.Drawing.Size(95, 23);
            this.buttonPowerOn.TabIndex = 15;
            this.buttonPowerOn.Text = "Power ON";
            this.buttonPowerOn.UseVisualStyleBackColor = false;
            // 
            // buttonPowerOff
            // 
            this.buttonPowerOff.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonPowerOff.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.buttonPowerOff.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.buttonPowerOff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPowerOff.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonPowerOff.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonPowerOff.Location = new System.Drawing.Point(214, 12);
            this.buttonPowerOff.Name = "buttonPowerOff";
            this.buttonPowerOff.Size = new System.Drawing.Size(95, 23);
            this.buttonPowerOff.TabIndex = 16;
            this.buttonPowerOff.Text = "Power OFF";
            this.buttonPowerOff.UseVisualStyleBackColor = false;
            this.buttonPowerOff.Click += new System.EventHandler(this.buttonPowerOff_Click);
            // 
            // listBoxProductsInstalled
            // 
            this.listBoxProductsInstalled.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxProductsInstalled.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.listBoxProductsInstalled.FormattingEnabled = true;
            this.listBoxProductsInstalled.Location = new System.Drawing.Point(248, 130);
            this.listBoxProductsInstalled.Name = "listBoxProductsInstalled";
            this.listBoxProductsInstalled.Size = new System.Drawing.Size(446, 264);
            this.listBoxProductsInstalled.TabIndex = 17;
            this.listBoxProductsInstalled.SelectedIndexChanged += new System.EventHandler(this.listBoxProductsInstalled_SelectedIndexChanged);
            // 
            // labelInstalledProducts
            // 
            this.labelInstalledProducts.AutoSize = true;
            this.labelInstalledProducts.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelInstalledProducts.Location = new System.Drawing.Point(245, 110);
            this.labelInstalledProducts.Name = "labelInstalledProducts";
            this.labelInstalledProducts.Size = new System.Drawing.Size(91, 13);
            this.labelInstalledProducts.TabIndex = 18;
            this.labelInstalledProducts.Text = "Installed Products";
            // 
            // labelPowerOff
            // 
            this.labelPowerOff.AutoSize = true;
            this.labelPowerOff.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelPowerOff.Location = new System.Drawing.Point(16, 261);
            this.labelPowerOff.Name = "labelPowerOff";
            this.labelPowerOff.Size = new System.Drawing.Size(113, 13);
            this.labelPowerOff.TabIndex = 19;
            this.labelPowerOff.Text = "VM List- Powered OFF";
            // 
            // listBoxPowerOff
            // 
            this.listBoxPowerOff.BackColor = System.Drawing.SystemColors.Menu;
            this.listBoxPowerOff.FormattingEnabled = true;
            this.listBoxPowerOff.Location = new System.Drawing.Point(15, 277);
            this.listBoxPowerOff.Name = "listBoxPowerOff";
            this.listBoxPowerOff.Size = new System.Drawing.Size(214, 186);
            this.listBoxPowerOff.TabIndex = 20;
            this.listBoxPowerOff.SelectedIndexChanged += new System.EventHandler(this.listBoxPowerOff_SelectedIndexChanged);
            // 
            // textBoxSearch
            // 
            this.textBoxSearch.Location = new System.Drawing.Point(495, 16);
            this.textBoxSearch.Name = "textBoxSearch";
            this.textBoxSearch.Size = new System.Drawing.Size(105, 20);
            this.textBoxSearch.TabIndex = 21;
            this.textBoxSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxSearch_KeyDown);
            // 
            // labelSearch
            // 
            this.labelSearch.AutoSize = true;
            this.labelSearch.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelSearch.Location = new System.Drawing.Point(364, 19);
            this.labelSearch.Name = "labelSearch";
            this.labelSearch.Size = new System.Drawing.Size(125, 13);
            this.labelSearch.TabIndex = 22;
            this.labelSearch.Text = "Search VMs with Version";
            // 
            // buttonSearch
            // 
            this.buttonSearch.BackColor = System.Drawing.Color.SteelBlue;
            this.buttonSearch.FlatAppearance.BorderColor = System.Drawing.Color.SteelBlue;
            this.buttonSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSearch.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonSearch.Location = new System.Drawing.Point(606, 15);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(95, 23);
            this.buttonSearch.TabIndex = 23;
            this.buttonSearch.Text = "Find";
            this.buttonSearch.UseVisualStyleBackColor = false;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(248, 451);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(446, 12);
            this.progressBar1.TabIndex = 24;
            this.progressBar1.Value = 10;
            this.progressBar1.Visible = false;
            // 
            // Progresslabel
            // 
            this.Progresslabel.AutoSize = true;
            this.Progresslabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Progresslabel.Location = new System.Drawing.Point(247, 434);
            this.Progresslabel.Name = "Progresslabel";
            this.Progresslabel.Size = new System.Drawing.Size(48, 13);
            this.Progresslabel.TabIndex = 25;
            this.Progresslabel.Text = "Progress";
            // 
            // Inventory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(706, 489);
            this.Controls.Add(this.Progresslabel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.labelSearch);
            this.Controls.Add(this.textBoxSearch);
            this.Controls.Add(this.listBoxPowerOff);
            this.Controls.Add(this.labelPowerOff);
            this.Controls.Add(this.labelInstalledProducts);
            this.Controls.Add(this.listBoxProductsInstalled);
            this.Controls.Add(this.buttonPowerOff);
            this.Controls.Add(this.buttonPowerOn);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.listVM);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbHost);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Inventory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QA Virtual Machines Inventory";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Inventory_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ListBox lbHost;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.ListBox listVM;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonPowerOn;
        private System.Windows.Forms.Button buttonPowerOff;
        private System.Windows.Forms.ListBox listBoxProductsInstalled;
        private System.Windows.Forms.Label labelInstalledProducts;
        private System.Windows.Forms.Label labelPowerOff;
        private System.Windows.Forms.ListBox listBoxPowerOff;
        private System.Windows.Forms.TextBox textBoxSearch;
        private System.Windows.Forms.Label labelSearch;
        private System.Windows.Forms.Button buttonSearch;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label Progresslabel;
    }
}

