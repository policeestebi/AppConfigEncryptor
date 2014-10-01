using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace AppConfigEncryptor.BLL
{
    /// <summary>
    /// Based in the code of Preetam U Ramdhave,
    /// URL http://www.codeproject.com/Tips/598863/EncryptionplusDecryptionplusConnectionplusStringpl
    /// </summary>

    public class AppConfigEncryptorBlo
    {

        #region Constructor
        
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void EncryptFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                EncryptConnectionString(true, fileName);
            }
            else
            {
                throw new System.IO.FileNotFoundException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public void DecryptFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                EncryptConnectionString(false, fileName);
            }
            else
            {
                throw new System.IO.FileNotFoundException();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encrypt"></param>
        /// <param name="fileName"></param>
        private  void EncryptConnectionString(bool encrypt, string fileName)
        {
            Configuration configuration = null;
            try
            {
                // Open the configuration file and retrieve the connectionStrings section.
                configuration = ConfigurationManager.OpenExeConfiguration(fileName);
                ConnectionStringsSection configSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
                if ((!(configSection.ElementInformation.IsLocked)) && (!(configSection.SectionInformation.IsLocked)))
                {
                    if (encrypt && !configSection.SectionInformation.IsProtected)
                    {
                        //this line will encrypt the file
                        configSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    }

                    if (!encrypt && configSection.SectionInformation.IsProtected)//encrypt is true so encrypt
                    {
                        //this line will decrypt the file. 
                        configSection.SectionInformation.UnprotectSection();
                    }
                    //re-save the configuration file section
                    configSection.SectionInformation.ForceSave = true;
                    // Save the current configuration

                    configuration.Save();
                    Process.Start("notepad.exe", configuration.FilePath);
                    //configFile.FilePath 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Properties

        #endregion

        #region Attributes
        
        #endregion
    }
}
