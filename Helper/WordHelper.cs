using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Win32.SafeHandles;
using NPOI.OpenXml4Net.OPC;
using NPOI.SS.UserModel;
using NPOI.XWPF.UserModel;

public class WordHelper
{
    private XWPFDocument doc = null;
    private string docFile = null;

    private static int SHEET_NUM = 0;
    private static int ROW_NUM = 0;
    private static int CELL_NUM = 0;
    private static double NEW_VALUE = 100.98D;
    private static String BINARY_EXTENSION = "xls";
    private static String OPENXML_EXTENSION = "xlsx";

    public WordHelper(string path)
    {
        UpdateEmbeddedDoc(path);
        UpdateEmbeddedDoc1();
        CheckUpdatedDoc();
    }

    public void CheckUpdatedDoc()
    {
        IWorkbook workbook = null;
        ISheet sheet = null;
        IRow row = null;
        NPOI.SS.UserModel.ICell cell = null;
        PackagePart pPart = null;
        IEnumerator<PackagePart> pIter = null;
        List<PackagePart> embeddedDocs = this.doc.GetAllEmbedds();
        if (embeddedDocs != null && embeddedDocs.Count != 0)
        {
            pIter = embeddedDocs.GetEnumerator();
            while (pIter.MoveNext())
            {
                pPart = pIter.Current;
                if (pPart.PartName.Extension.Equals(BINARY_EXTENSION) ||
                        pPart.PartName.Extension.Equals(OPENXML_EXTENSION))
                {
                    workbook = WorkbookFactory.Create(pPart.GetInputStream());
                    sheet = workbook.GetSheetAt(SHEET_NUM);
                    row = sheet.GetRow(ROW_NUM);
                    cell = row.GetCell(CELL_NUM);
                    // Assert.AreEqual(cell.NumericCellValue, NEW_VALUE);
                }
            }
        }
    }

    public void UpdateEmbeddedDoc(String filename)
    {
        this.docFile = filename;
        FileStream fis = null;
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException("The Word dcoument " +
                    filename +
                    " does not exist.");
        }
        try
        {

            // Open the Word document file and instantiate the XWPFDocument
            // class.
            fis = new FileStream(this.docFile, FileMode.Open);
            this.doc = new XWPFDocument(fis);
        }
        finally
        {
            if (fis != null)
            {
                try
                {
                    fis.Close();
                    fis = null;
                }
                catch (IOException)
                {
                    System.Console.WriteLine("IOException caught trying to close " +
                            "FileInputStream in the constructor of " +
                            "UpdateEmbeddedDoc.");
                }
            }
        }
    }

    public void UpdateEmbeddedDoc1()
    {

        IList<XWPFTable> embeddedDocs = this.doc.Tables;
        if (embeddedDocs != null && embeddedDocs.Count != 0)
        {
            // foreach (var para in doc.Paragraphs)
            // {
            //     ReplaceKey(para);
            // }
            //遍历表格
            var tables = doc.Tables;
            // foreach (var table in tables)
            // {
            //     foreach (var row in table.Rows)
            //     {
            //         foreach (var cell in row.GetTableCells())
            //         {
            //             foreach (var para in cell.Paragraphs)
            //             {
            //                 ReplaceKey(para);
            //             }
            //         }
            //     }
            // }
            //表格内容
            var tablesChild = doc.Tables[0].Rows[0].GetCell(1).Tables;
            //第二排所有文字
            var paragraphs = tablesChild[0].Rows[1].GetCell(0).Paragraphs;
            var txtList = paragraphs.Select(x => x.ParagraphText).ToList();

            var cell = tablesChild[0].Rows[2].GetCell(0);
            XWPFParagraph p3 = cell.AddParagraph();
            p3.IndentFromLeft=113;
            p3.IndentFromRight=113;
            p3.Alignment=ParagraphAlignment.CENTER;
            var r= p3.CreateRun();
            r.SetText("翁志来");
            r.FontFamily="黑体";
            r.FontSize=18;

            XWPFParagraph p4 = cell.AddParagraph();
            p4.IndentFromLeft=113;
            p4.IndentFromRight=113;
            var r4= p4.CreateRun();
            r4.SetText("生殁未详生殁未详生殁未详生殁未详");
            r4.FontFamily="楷体";
            r4.FontSize=14;

        //    tablesChild[0].Rows[2].GetCell(0).Paragraphs[0].Runs[0].SetText("第二十七");

            // tablesChild[0].Rows[2].GetCell(0).SetText("asdfadf");

            // ReplaceKeyTable(doc.Tables);

        }

        // Finally, write the newly modified Word document out to file.
        string file = Path.GetFileNameWithoutExtension(this.docFile) + "tmp" + Path.GetExtension(this.docFile);
        this.doc.Write(new FileStream(file, FileMode.CreateNew));
        this.doc.Close();
        this.doc=null;
    }

    private void ReplaceKeyTable(IList<XWPFTable> tables)
    {
        foreach (var table in tables)
        {
            foreach (var row in table.Rows)
            {
                foreach (var cell in row.GetTableCells())
                {
                    foreach (var para in cell.Paragraphs)
                    {
                        ReplaceKey(para);
                    }

                    ReplaceKeyTable(cell.Tables);
                }
            }
        }
    }

    private void ReplaceKey(XWPFParagraph para)
    {

        string text = para.ParagraphText;
        var runs = para.Runs;
        string styleid = para.Style;
        for (int i = 0; i < runs.Count; i++)
        {
            var run = runs[i];
            text = run.ToString();
            runs[i].SetText(text + 2, 0);
        }
    }

}