using UnityEngine;
using TMPro;
using System.Collections;
using System.Text.RegularExpressions;

public class DialogueTextFlow : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public float textSpeed;

    private string[] lines;  
    private int index;

    public void SetDialogueLines(string[] newLines)
    {
        lines = newLines;
    }
    
    public void StartDialogue()
    {
        index = 0;
        textComponent.text = string.Empty;
        gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (lines == null || index < 0 || index >= lines.Length) 
                return;

            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    IEnumerator TypeLine()
    {
        if (lines == null || index < 0 || index >= lines.Length)
            yield break;

        string line = lines[index];
        int i = 0;
        bool inTag = false;
        string currentText = "";

        while (i < line.Length)
        {
            if (line[i] == '<')
            {
                int tagEnd = line.IndexOf('>', i);
                if (tagEnd == -1) tagEnd = line.Length - 1;
                
                currentText += line.Substring(i, tagEnd - i + 1);
                textComponent.text = currentText;
                
                i = tagEnd + 1;
                continue;
            }

            currentText += line[i];
            textComponent.text = currentText;
            i++;
            
            if (!inTag)
            {
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            lines = null; 
        }
    }
}