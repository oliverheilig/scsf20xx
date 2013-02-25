//===============================================================================
// Microsoft patterns & practices
// Smart Client Software Factory 2010
//===============================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SqlRepositoryAdmin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void filesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.filesBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.fileRepositoryDataSet);

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'fileRepositoryDataSet.Files' table. You can move, or remove it, as needed.
            this.filesTableAdapter.Fill(this.fileRepositoryDataSet.Files);

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            // Prompt for the file path
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "All files(*.*)|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string fileName = ofd.FileName;
                FileRepositoryDataSet.FilesRow newRow = fileRepositoryDataSet.Files.NewFilesRow();
                newRow.FilePath = Path.GetFileName(ofd.FileName);
                newRow.OriginalPath = ofd.FileName;
                newRow.Modified = DateTime.Now;
                byte[] bits;
                using (FileStream fstream = File.OpenRead(ofd.FileName))
                {
                    bits = new byte[fstream.Length];
                    fstream.Read(bits, 0, (int)fstream.Length);
                }
                newRow.FileBits = bits;
                fileRepositoryDataSet.Files.AddFilesRow(newRow);
            }
            ofd.Dispose();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Validate();
            filesBindingSource.EndEdit();
            filesTableAdapter.Update(fileRepositoryDataSet.Files);
        }
    }
}
