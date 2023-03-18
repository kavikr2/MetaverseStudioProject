    using SimpleJSON;
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Networking;

    public class SM_LeaderBoard : MonoBehaviour
    {
        public int Aviation,
        Maritime,
        Healthcare,
        Education;
        public TextMeshProUGUI text;
        string url = "https://sheets.googleapis.com/v4/spreadsheets/1fPiZE0_L-vgQytUzXH9tw_jOp_zQxYMD6joIIA8Yens/values/Sheet2?key=AIzaSyDcGDo4WpnQFPvbwTxuETtR-jHWlhHLmCE";

        private void Awake() { StartCoroutine(GetData()); }
        private void OnDisable() { StopCoroutine(GetData()); UpdateData(); }

        private void Update()
        {
            text.SetText("Aviation :" + Aviation.ToString() + "<br>" + "Maritime :" + Maritime.ToString() + "<br>" + "Healthcare :" + Healthcare.ToString() + "<br>" + "Education :" + Education.ToString() + "<br>");
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
                    Aviation = int.Parse(rowData["Aviation"]);
                    Maritime = int.Parse(rowData["Maritime"]);
                    Healthcare = int.Parse(rowData["Healthcare"]);
                    Education = int.Parse(rowData["Education"]);
                }
            }
        }

    public void EnditPls()
    {
        StopCoroutine(GetData());
        StopAllCoroutines();
    }
    public void UpdateData()
    {
        Dictionary<string, string> newRow = new Dictionary<string, string>();

        newRow["Aviation"] = Aviation.ToString();
        newRow["Maritime"] = Maritime.ToString();
        newRow["Healthcare"] = Healthcare.ToString();
        newRow["Education"] = Education.ToString();


        JSONNode rowJson = JSON.Parse("{}");
        foreach (KeyValuePair<string, string> kvp in newRow)
        {
            rowJson[kvp.Key] = kvp.Value;
        }
        string rowJsonString = rowJson.ToString();

        UnityWebRequest request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes("{\"range\":\"Sheet2\",\"majorDimension\":\"ROWS\",\"values\":" + "[" + rowJsonString + "]" + "}");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("Done Updating");
        }
    }
}
