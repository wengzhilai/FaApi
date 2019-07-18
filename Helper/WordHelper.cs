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
    private static int SHEET_NUM = 0;
    private static int ROW_NUM = 0;
    private static int CELL_NUM = 0;
    private static double NEW_VALUE = 100.98D;
    private static String BINARY_EXTENSION = "xls";
    private static String OPENXML_EXTENSION = "xlsx";

    public WordHelper()
    {

        // CheckUpdatedDoc();
    }

    public XWPFDocument MakeXWPFDocument(String filename)
    {
        FileStream fis = null;
        if (!File.Exists(filename))
        {
            throw new FileNotFoundException("文档 " + filename + " 不存在.");
        }
        try
        {
            fis = new FileStream(filename, FileMode.Open);
            return new XWPFDocument(fis);
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

    public XWPFDocument UpdateEmbeddedDoc1(XWPFDocument doc, int tableIndex, int rowIndex)
    {
        var cell = doc.Tables[1].Rows[0].GetCell(0).Tables[0].Rows[1].GetCell(0);
        cell.RemoveParagraph(0);
        this.AddElder(cell, "第七世");
        this.AddName(cell, "翁志来");
        this.AddRemark(cell, "出生于1981年3月13");
        return doc;
    }

    public void SaveDoc(XWPFDocument doc, string savePath)
    {
        var fs = new FileStream(savePath, FileMode.Create);
        doc.Write(fs);
        fs.Close();
        fs.Dispose();
        doc.Close();
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

    /// <summary>
    /// 添加辈份
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="txt"></param>
    public void AddElder(XWPFTableCell cell, string txt)
    {
        //清除表格的第一个元素
        if (cell.Paragraphs != null && cell.Paragraphs.Count() == 1 && cell.Paragraphs[0].Runs.Count == 0)
        {
            cell.RemoveParagraph(0);
        }
        XWPFParagraph p3 = cell.AddParagraph();
        p3.IndentFromLeft = 113;
        p3.IndentFromRight = 113;
        p3.Alignment = ParagraphAlignment.CENTER;
        var r = p3.CreateRun();
        r.GetCTR().AddNewRPr().bdr = new NPOI.OpenXmlFormats.Wordprocessing.CT_Border()
        {
            val = NPOI.OpenXmlFormats.Wordprocessing.ST_Border.thinThickSmallGap,
            sz = 24
        };
        r.SetText(txt);
        r.FontFamily = "黑体";
        r.FontSize = 18;
    }

    public void AddNavigation(XWPFTableCell cell, string txt)
    {
        //清除表格的第一个元素
        if (cell.Paragraphs != null && cell.Paragraphs.Count() == 1 && cell.Paragraphs[0].Runs.Count == 0)
        {
            cell.RemoveParagraph(0);
        }
        XWPFParagraph p3 = cell.AddParagraph();
        p3.IndentFromLeft = 113;
        p3.IndentFromRight = 113;
        p3.Alignment = ParagraphAlignment.RIGHT;
        var r = p3.CreateRun();
        r.SetText(txt);
        r.FontFamily = "楷体";
        r.FontSize = 14;
    }

    /// <summary>
    /// 添加名称
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="txt"></param>
    public void AddName(XWPFTableCell cell, string txt)
    {
        //清除表格的第一个元素
        if (cell.Paragraphs != null && cell.Paragraphs.Count() == 1 && cell.Paragraphs[0].Runs.Count == 0)
        {
            cell.RemoveParagraph(0);
        }
        XWPFParagraph p3 = cell.AddParagraph();
        p3.IndentFromLeft = 113;
        p3.IndentFromRight = 113;
        p3.Alignment = ParagraphAlignment.CENTER;
        var r = p3.CreateRun();
        r.SetText(txt);
        r.FontFamily = "黑体";
        r.FontSize = 18;
    }
    /// <summary>
    /// 添加说明
    /// </summary>
    /// <param name="cell"></param>
    /// <param name="txt"></param>
    public void AddRemark(XWPFTableCell cell, string txt)
    {
        //清除表格的第一个元素
        if (cell.Paragraphs != null && cell.Paragraphs.Count() == 1 && cell.Paragraphs[0].Runs.Count == 0)
        {
            cell.RemoveParagraph(0);
        }
        XWPFParagraph p4 = cell.AddParagraph();
        p4.IndentFromLeft = 113;
        p4.IndentFromRight = 113;
        var r4 = p4.CreateRun();
        r4.SetText(txt);
        r4.FontFamily = "楷体";
        r4.FontSize = 14;
    }

    public void AddPageNum(XWPFTableCell cell, string txt)
    {
        //清除表格的第一个元素
        if (cell.Paragraphs != null && cell.Paragraphs.Count() == 1 && cell.Paragraphs[0].Runs.Count == 0)
        {
            cell.RemoveParagraph(0);
        }
        XWPFParagraph p4 = cell.AddParagraph();
        p4.Alignment = ParagraphAlignment.CENTER;
        var r4 = p4.CreateRun();
        r4.SetText(txt);
        r4.FontFamily = "楷体";
        r4.FontSize = 14;
    }

}