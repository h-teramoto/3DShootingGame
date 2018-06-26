using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleStartButtonObserver
{
    private TitleController _titleController;

    public TitleStartButtonObserver(TitleController titleController)
    {
        _titleController = titleController;
    }

    public void Observe()
    {
        _titleController.StartButton.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        SceneManager.LoadScene("Main");
    }
}
