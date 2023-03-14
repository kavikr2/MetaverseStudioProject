using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;

public class SM_Accounts : MonoBehaviour
{
    string url = "https://sheets.googleapis.com/v4/spreadsheets/1fPiZE0_L-vgQytUzXH9tw_jOp_zQxYMD6joIIA8Yens/values/Sheet1?key=AIzaSyDcGDo4WpnQFPvbwTxuETtR-jHWlhHLmCE";

    void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string json = request.downloadHandler.text;
            JSONNode data = JSON.Parse(json);

            List<string> columns = new List<string>();

            foreach (JSONNode value in data["values"][0].AsArray)
            {
                string columnName = value.Value;
                columns.Add(columnName);
            }

            List<Dictionary<string, string>> rowsData = new List<Dictionary<string, string>>();

            for (int i = 1; i < data["values"].Count; i++)
            {
                JSONNode row = data["values"][i];
                Dictionary<string, string> rowData = new Dictionary<string, string>();

                for (int j = 0; j < row.Count; j++)
                {
                    string columnName = columns[j];
                    string value = row[j].Value;
                    rowData[columnName] = value;
                }

                rowsData.Add(rowData);
            }

            foreach (Dictionary<string, string> rowData in rowsData)
            {
                string name = rowData["Name"];
                string email = rowData["Email"];
                string password = rowData["Password"];

                Debug.Log("Name: " + name + ", Email: " + email + ", Password: " + password);
            }
        }
    }

    IEnumerator UpdateData(string name, string email, string password)
    {
        Dictionary<string, string> newRow = new Dictionary<string, string>();
        newRow["Name"] = name;
        newRow["Email"] = email;
        newRow["Password"] = password;

        JSONNode rowJson = JSON.Parse("{}");
        foreach (KeyValuePair<string, string> kvp in newRow)
        {
            rowJson[kvp.Key] = kvp.Value;
        }
        string rowJsonString = rowJson.ToString();

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes("{\"range\":\"Sheet1\",\"majorDimension\":\"ROWS\",\"values\":" + "[" + rowJsonString + "]" + "}");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Data updated successfully.");
        }
    }
    
}