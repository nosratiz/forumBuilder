using System;
using System.Collections.Generic;
using System.IO;
using ClosedXML.Excel;
using DNTPersianUtils.Core;
using FourmBuilder.Api.Core.Models;
using FourmBuilder.Common.Helper.Environment;

namespace FourmBuilder.Api.Core.Excel
{
    public static class ReportGenerator
    {
        public static string GenerateForumAnswers(List<UserResponse> userResponses, List<string> questions)
        {
            try
            {
                using var workbook = new XLWorkbook();

                IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Forum");
                worksheet.RightToLeft = true;
                worksheet.ColumnWidth = 20;
                worksheet.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;


                worksheet.Cell(1, 1).Value = "ردیف";
                worksheet.Cell(1, 2).Value = "نام و نام خانوادگی";
                worksheet.Cell(1, 3).Value = "تاریخ پاسخ";
                for (int j = 4; j < (questions.Count + 4); j++)
                    worksheet.Cell(1, j).Value = questions[j - 4];


                for (int index = 1; index <= userResponses.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = index;
                    worksheet.Cell(index + 1, 2).Value = userResponses[index - 1].User != null
                        ? $"{userResponses[index - 1].User.FirstName} {userResponses[index - 1].User.LastName}"
                        : "ناشناس";
                    worksheet.Cell(index + 1, 3).Value =
                        userResponses[index - 1].CreateDate.ToLongPersianDateTimeString();
                    for (int i = 4; i < (userResponses[index - 1].Answers.Count + 4); i++)
                    {
                        if (userResponses[index - 1].Answers[i - 4].StartsWith("/Files"))
                        {
                            var filePathName = userResponses[index - 1].Answers[i - 4]
                                .Replace("/Files/Doc/", "/Uploads/Documents/");

                            var filepath = Path.Combine(Directory.GetCurrentDirectory() +
                                                        $"{filePathName}");

                            worksheet.AddPicture(filepath)
                                .MoveTo(worksheet.Cell(index + 1, i))
                                .Scale(0.1)
                                .ScaleHeight(0.1)
                                .ScaleHeight(0.1)
                                .WithSize(100, 100);
                        }
                        else
                        {
                            worksheet.Cell(index + 1, i).Value = userResponses[index - 1].Answers[i - 4];
                        }
                    }
                }


                var uniqueName = $" لیست پاسخ ها - {DateTime.Now.ToLongPersianDateString()}.xlsx";
                var fileName = $"{ApplicationStaticPath.Documents}/{uniqueName}";

                workbook.SaveAs(fileName);

                return $"{ApplicationStaticPath.Clients.Document}/{uniqueName}";
            }
            catch (Exception)
            {
                // ignored
            }

            return string.Empty;
        }
    }
}