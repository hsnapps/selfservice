namespace SelfService
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.registered = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.completed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.authorized_units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.course_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.course_symbol = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gpa = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passed_units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.required_units = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passed_subjects = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.required_subjects = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.registered,
            this.completed,
            this.authorized_units,
            this.course_name,
            this.course_symbol,
            this.gpa,
            this.passed_units,
            this.required_units,
            this.passed_subjects,
            this.required_subjects});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.dataGridView1.Size = new System.Drawing.Size(1333, 749);
            this.dataGridView1.TabIndex = 0;
            // 
            // registered
            // 
            this.registered.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.registered.DataPropertyName = "registered";
            this.registered.HeaderText = "مسجل حاليا";
            this.registered.Name = "registered";
            this.registered.ReadOnly = true;
            this.registered.Width = 97;
            // 
            // completed
            // 
            this.completed.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.completed.DataPropertyName = "completed";
            this.completed.HeaderText = "مستوفى";
            this.completed.Name = "completed";
            this.completed.ReadOnly = true;
            this.completed.Width = 75;
            // 
            // authorized_units
            // 
            this.authorized_units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.authorized_units.DataPropertyName = "authorized_units";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.authorized_units.DefaultCellStyle = dataGridViewCellStyle1;
            this.authorized_units.HeaderText = "الوحدات المعتمدة للمقرر";
            this.authorized_units.Name = "authorized_units";
            this.authorized_units.ReadOnly = true;
            this.authorized_units.Width = 154;
            // 
            // course_name
            // 
            this.course_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.course_name.DataPropertyName = "course_name";
            this.course_name.HeaderText = "إسم المقرر";
            this.course_name.Name = "course_name";
            this.course_name.ReadOnly = true;
            this.course_name.Width = 85;
            // 
            // course_symbol
            // 
            this.course_symbol.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.course_symbol.DataPropertyName = "course_symbol";
            this.course_symbol.HeaderText = "رمز المقرر";
            this.course_symbol.Name = "course_symbol";
            this.course_symbol.ReadOnly = true;
            this.course_symbol.Width = 83;
            // 
            // gpa
            // 
            this.gpa.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.gpa.DataPropertyName = "gpa";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.gpa.DefaultCellStyle = dataGridViewCellStyle2;
            this.gpa.HeaderText = "المعدل التراكمي";
            this.gpa.Name = "gpa";
            this.gpa.ReadOnly = true;
            this.gpa.Width = 119;
            // 
            // passed_units
            // 
            this.passed_units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.passed_units.DataPropertyName = "passed_units";
            this.passed_units.HeaderText = "الوحدات المعتمدة المنجزة";
            this.passed_units.Name = "passed_units";
            this.passed_units.ReadOnly = true;
            this.passed_units.Width = 155;
            // 
            // required_units
            // 
            this.required_units.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.required_units.DataPropertyName = "required_units";
            this.required_units.HeaderText = "الوحدات المعتمدة المطلوبة";
            this.required_units.Name = "required_units";
            this.required_units.ReadOnly = true;
            this.required_units.Width = 158;
            // 
            // passed_subjects
            // 
            this.passed_subjects.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.passed_subjects.DataPropertyName = "passed_subjects";
            this.passed_subjects.HeaderText = "عدد المقررات المنجزة";
            this.passed_subjects.Name = "passed_subjects";
            this.passed_subjects.ReadOnly = true;
            this.passed_subjects.Width = 136;
            // 
            // required_subjects
            // 
            this.required_subjects.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.required_subjects.DataPropertyName = "required_subjects";
            this.required_subjects.HeaderText = "عدد المقررات المطلوبة";
            this.required_subjects.Name = "required_subjects";
            this.required_subjects.ReadOnly = true;
            this.required_subjects.Width = 139;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1333, 749);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("AL-Mohanad", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(178)));
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn registered;
        private System.Windows.Forms.DataGridViewTextBoxColumn completed;
        private System.Windows.Forms.DataGridViewTextBoxColumn authorized_units;
        private System.Windows.Forms.DataGridViewTextBoxColumn course_name;
        private System.Windows.Forms.DataGridViewTextBoxColumn course_symbol;
        private System.Windows.Forms.DataGridViewTextBoxColumn gpa;
        private System.Windows.Forms.DataGridViewTextBoxColumn passed_units;
        private System.Windows.Forms.DataGridViewTextBoxColumn required_units;
        private System.Windows.Forms.DataGridViewTextBoxColumn passed_subjects;
        private System.Windows.Forms.DataGridViewTextBoxColumn required_subjects;
    }
}

