using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnnouncementHandler : MonoBehaviour
{
    public TextMeshProUGUI announcementText;

    private struct Announcement
    {
        public string text;
        public float time;
    }

    private Queue<Announcement> announcementQueue = new();

    private Vector3 hiddenPosition;
    private Vector3 showingPosition;
    private bool showing = false;

    public void CreateAnnouncement(string _text, float _time=3.0f)
    {
        Announcement an;
        an.text = _text;
        an.time = _time;
        announcementQueue.Enqueue(an);

        if (!showing)
        {
            StartCoroutine(StartAnnouncementAnim());
        }
    }

    private IEnumerator StartAnnouncementAnim()
    {
        showing = true;
        SetAnnounceText(announcementQueue.Peek().text);
        LeanTween.move(gameObject, showingPosition, 0.25f).setEase(LeanTweenType.easeInOutSine);
        yield return new WaitForSeconds(announcementQueue.Peek().time);
        LeanTween.move(gameObject, hiddenPosition, 0.25f).setEase(LeanTweenType.easeInOutSine);
        yield return new WaitForSeconds(0.5f);
        announcementQueue.Dequeue();

        if (announcementQueue.Count == 0)
        {
            showing = false;
        }
        else
        {
            StartCoroutine(StartAnnouncementAnim());
        }
    }

    private void SetAnnounceText(string text)
    {
        announcementText.text = text;
    }

}
