using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MetaData.Methods
{
    public class ErrorLogs
    {
        string errorPath = "";
        string drive = "E:\\";
        public ErrorLogs()
        {
            if (!Directory.Exists(drive))
            {
                drive = "C:\\";
            }
        }
        public void LogExceptions(Exception exc, string source)
        {
            try
            {
                string Log_file = "MetaDataErrorLogs.log";
                //errorPath = Path.Combine(_hostingEnvironment.ContentRootPath, "apperrors\\");
                string error_date = DateTime.Now.ToString("yyyy-MM-dd");
                errorPath = Path.Combine(drive, "Logs\\metaybackgrounderrors\\" + error_date + "\\");
                if (!System.IO.Directory.Exists(errorPath))
                {
                    System.IO.Directory.CreateDirectory(errorPath);
                }
                string CurrentPath = errorPath;// _hostingEnvironment.ContentRootPath;
                string File_PATHS = Path.Combine(CurrentPath, Log_file);

                using (StreamWriter SW = File.AppendText(File_PATHS))
                {
                    SW.WriteLine("********************{0}*******************", DateTime.Now);
                    if (exc.Message != null)
                    {
                        SW.Write("Error Message: ");
                        SW.Write(exc.Message.GetType().ToString());
                        SW.Write("Error Exception: ");
                        SW.Write(exc.Message);
                        SW.Write("Error Message Source");
                        SW.WriteLine(exc.Source);
                        SW.WriteLine(source);
                        SW.WriteLine("");
                        SW.Close();
                    }
                    else
                    {
                        if (exc.InnerException != null)
                        {
                            SW.Write("Inner Exception type: ");
                            SW.Write(exc.InnerException.GetType().ToString());
                            SW.Write("Inner Exception: ");
                            SW.Write(exc.InnerException.Message);
                            SW.Write("Inner Exception Source");
                            SW.WriteLine(exc.InnerException.Source);
                            if (exc.InnerException.StackTrace != null)
                            {
                                SW.WriteLine("Inner Stack Trace: ");
                                SW.WriteLine(exc.InnerException.StackTrace);
                            }
                            SW.Write("Exception Type");
                            SW.WriteLine(exc.GetType().ToString());
                            SW.WriteLine("Exception " + exc.Message);
                            SW.WriteLine("Source " + source);
                            SW.WriteLine("Stack Trace: ");
                            SW.WriteLine("");
                            if (exc.StackTrace != null)
                            {
                                SW.WriteLine(exc.StackTrace);
                                SW.WriteLine();
                            }
                            SW.Close();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                return;
            }

        }
        public void Logs(string start_time, string end_time, int total_executed, string execution_time)
        {
            try
            {
                string Log_file = "MetaDataLog.log";
                //errorPath = Path.Combine(_hostingEnvironment.ContentRootPath, "apperrors\\");
                string error_date = DateTime.Now.ToString("yyyy-MM-dd");
                errorPath = Path.Combine(drive, "Logs\\metabackground\\" + error_date + "\\");
                if (!System.IO.Directory.Exists(errorPath))
                {
                    System.IO.Directory.CreateDirectory(errorPath);
                }
                string CurrentPath = errorPath;// _hostingEnvironment.ContentRootPath;
                string File_PATHS = Path.Combine(CurrentPath, Log_file);

                using (StreamWriter SW = File.AppendText(File_PATHS))
                {
                    SW.WriteLine("********************{0}*******************", DateTime.Now);

                    SW.WriteLine("START TIME: ");
                    SW.WriteLine(start_time);
                    SW.WriteLine("");
                    SW.WriteLine("END TIME: ");
                    SW.WriteLine(end_time);
                    SW.WriteLine("");
                    SW.WriteLine("TOTAL EXECUTED");
                    SW.WriteLine(total_executed);
                    SW.WriteLine("");
                    SW.WriteLine("EXECUTION TIME");
                    SW.WriteLine(execution_time);
                    SW.Close();


                }
            }
            catch (Exception e)
            {
                return;
            }

        }
    }
}
