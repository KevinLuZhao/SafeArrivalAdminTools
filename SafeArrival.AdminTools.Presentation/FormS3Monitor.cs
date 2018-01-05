using SafeArrival.AdminTools.AwsUtilities;
using SafeArrival.AdminTools.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SafeArrival.AdminTools.Presentation
{
    public partial class FormS3Monitor : FormMdiChildBase
    {
        Dictionary<string, List<SA_S3Object>> _envAwsS3ObjectList = new Dictionary<string, List<SA_S3Object>>();
        TreeNode _currentNode = null;
        TreeNode _prevNode = null;

        public FormS3Monitor()
        {
            InitializeComponent();
        }

        public override void OnEnvironmentChanged()
        {
            try
            {
                FormS3Monitor_Load(this, new EventArgs());
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void FormS3Monitor_Load(object sender, EventArgs e)
        {
            try
            {
                InitTree();
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void tvS3Folders_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                var node = e.Node;
                //If the node is S3 Bucket and not be loaded yet
                if (node.Parent == null && (node.Nodes == null || node.Nodes.Count == 0))
                {
                    LoadBucketTree(node);
                }
                PopulateGvFileList(node);
                _prevNode = _currentNode;
                _currentNode = node;
                if (_prevNode != null)
                    _prevNode.BackColor = System.Drawing.Color.Transparent;
                if (_currentNode != null)
                    _currentNode.BackColor = System.Drawing.Color.Gray;
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var s3BucketName = GetS3BucketName(_currentNode);
                S3Helper s3Helper = new S3Helper(
                           GlobalVariables.Enviroment,
                           GlobalVariables.Region,
                           s3BucketName
                           );
                if (dgViewS3Files.SelectedRows.Count > 0)
                {
                    DialogResult result = folderBrowserDialog1.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        string folder = folderBrowserDialog1.SelectedPath;
                        foreach (var row in dgViewS3Files.SelectedRows)
                        {
                            var s3FilePath = ((DataGridViewRow)row).Cells[0].Value.ToString();
                            string fileName = Path.Combine(folder, Path.GetFileName(s3FilePath));
                            var stream = await s3Helper.DownloadFile(s3FilePath);
                            using (Stream file = File.Create(fileName))
                            {
                                CopyStream(stream, file);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var s3BucketName = GetS3BucketName(_currentNode);
                S3Helper s3Helper = new S3Helper(
                           GlobalVariables.Enviroment,
                           GlobalVariables.Region,
                           s3BucketName
                           );
                DialogResult result = openFileDialog1.ShowDialog();
                if (result == DialogResult.OK)
                {
                    string filePath = openFileDialog1.FileName;
                    var s3FilePath =
                        _currentNode.FullPath.Substring(_currentNode.FullPath.IndexOf("\\") + 1) +
                        "/" + Path.GetFileName(openFileDialog1.FileName);
                    var stream = new FileStream(filePath, FileMode.Open);
                    await s3Helper.UploadFile(s3FilePath, stream);
                    RefreshGridviewControl();
                    WriteNotification(string.Format("File {0} was uploaded to {1}", Path.GetFileName(s3FilePath), s3BucketName));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private async void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var s3BucketName = GetS3BucketName(_currentNode);
            StringBuilder sb = new StringBuilder();
            S3Helper s3Helper = new S3Helper(
                       GlobalVariables.Enviroment,
                       GlobalVariables.Region,
                       s3BucketName
                       );
            try
            {
                if (dgViewS3Files.SelectedRows.Count > 0)
                {
                    StringBuilder deletedFiles = new StringBuilder();
                    foreach (var row in dgViewS3Files.SelectedRows)
                    {
                        deletedFiles.Append(((DataGridViewRow)row).Cells[0].Value.ToString() + ",");
                    }
                    var confirmResult = MessageBox.Show(
                        string.Format("Are you sure to delete file(s): '{0}' from S3 Bucket?",
                        deletedFiles.ToString().Substring(0, deletedFiles.ToString().Length - 1)),
                        "Confirm Delete S3 Bucket Files",
                        MessageBoxButtons.YesNo);
                    if (confirmResult == DialogResult.Yes)
                    {
                        foreach (var row in dgViewS3Files.SelectedRows)
                        {
                            var s3FilePath = ((DataGridViewRow)row).Cells[0].Value.ToString();
                            sb.Append((((DataGridViewRow)row).Cells[0].Value.ToString()) + ",");
                            await s3Helper.DeleteFile(s3FilePath);
                        }
                        RefreshGridviewControl();
                    }
                    WriteNotification(string.Format("Files {0} were deleted from {1}", sb.ToString(), s3BucketName));
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_currentNode == null)
                    return;
                RefreshGridviewControl();
            }
            catch (Exception ex)
            {
                FormS3Monitor_Load(this, new EventArgs());
            }
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[8 * 1024];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        private void InitTree()
        {
            _envAwsS3ObjectList.Clear();
            tvS3Folders.Nodes.Clear();
            List<string> bucketList = new List<string>();
            bucketList.Add(string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "artifact"));
            bucketList.Add(string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "parameters"));
            bucketList.Add(string.Join("-", "safe-arrival", GlobalVariables.Region, GlobalVariables.Enviroment, "sisbucket"));

            foreach (var s3BucketName in bucketList)
            {
                TreeNode bucketRootNode = new TreeNode(s3BucketName);
                tvS3Folders.Nodes.Add(bucketRootNode);
                _envAwsS3ObjectList.Add(s3BucketName, new List<SA_S3Object>());
            }
        }

        private void LoadBucketTree(TreeNode bucketNode)
        {
            if (bucketNode.Parent != null)
            {
                NotifyToMainStatus(string.Format("{0} is not a S3 Bucket level node.", bucketNode.Text), System.Drawing.Color.Red);
                return;
            }
            var lst = RefreshS3BucketObjectList(bucketNode.Text);

            //TreeNode bucketRootNode = new TreeNode(s3BucketName);
            BuildTreeviewNode(lst, string.Empty, bucketNode);
            bucketNode.Expand();
        }

        private void RefreshGridviewControl()
        {
            var s3BucketName = GetS3BucketName(_currentNode);
            RefreshS3BucketObjectList(s3BucketName);
            PopulateGvFileList(_currentNode);
        }

        private List<SA_S3Object> RefreshS3BucketObjectList(string s3BucketName)
        {
            S3Helper s3Helper = new S3Helper(
                               GlobalVariables.Enviroment,
                               GlobalVariables.Region,
                               s3BucketName
                               );
            var s3ObjList = s3Helper.GetBucketFileList();
            _envAwsS3ObjectList[s3BucketName].Clear();
            _envAwsS3ObjectList[s3BucketName].AddRange(s3ObjList);
            return s3ObjList;
        }

        private void PopulateGvFileList(TreeNode node)
        {
            var selectedFolder = node.FullPath.Replace(@"\", "/") + "/";
            var s3BucketName = GetS3BucketName(node);
            var selectedObjList = _envAwsS3ObjectList[s3BucketName].FindAll(o =>
                string.Join("/", o.S3BucketName, o.FullName).IndexOf(selectedFolder) == 0 &&
                string.Join("/", o.S3BucketName, o.FullName).Replace(selectedFolder, "").IndexOf("/") < 0);
            var bindingList = new BindingList<SA_S3Object>(selectedObjList);
            dgViewS3Files.DataSource = bindingList;
            dgViewS3Files.Columns[1].Visible = false;
        }

        private void BuildTreeviewNode(List<SA_S3Object> s3ObjList, string parentFolder, TreeNode parentNode)
        {
            List<string> lstFolders = new List<string>();
            //All item list under the parent folder. For example: under application/, may have item like application/api/api1.zip 
            var subList = s3ObjList.FindAll(o => o.FullName.IndexOf(parentFolder) == 0);
            //All top folders under the parent folder. For above example: the top folder is api
            var subTopFolderList = new HashSet<string>();
            foreach (var obj in subList)
            {
                string subString = parentFolder == string.Empty ? obj.FullName : obj.FullName.Replace(parentFolder, string.Empty);
                if (!string.IsNullOrEmpty(subString) && subString.IndexOf("/") > 0)
                    subTopFolderList.Add(subString.Substring(0, subString.IndexOf("/") + 1));
            }

            foreach (var folder in subTopFolderList)
            {
                TreeNode node = new TreeNode();
                node.Text = folder.Replace("/", "");
                node.Tag = parentFolder + folder;
                BuildTreeviewNode(subList, node.Tag.ToString(), node);
                parentNode.Nodes.Add(node);
            }
        }

        private string GetS3BucketName(TreeNode node)
        {
            if (node.Parent != null)
            {
                return GetS3BucketName(node.Parent);
            }
            return node.FullPath;
        }
    }
}
