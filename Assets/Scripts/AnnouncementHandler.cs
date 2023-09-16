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

    private bool announcementsLocked = false;

    private void Awake()
    {
        Global.announcer = this;
        hiddenPosition = transform.position;
        showingPosition = transform.position + new Vector3(525, 0,0);
    }

    public void LockAnnouncer()
    {
        announcementsLocked = true;
    }

    public void UnlockAnnouncer()
    {
        announcementsLocked = false;
    }

    public void ClearQueue()
    {
        announcementQueue.Clear();
        if (showing) 
        {
            transform.localScale = Vector3.zero;
        }
        
        showing = false;
    }

    public void CreateAnnouncement(string _text, float _time=3.0f)
    {
        if (announcementsLocked)
        {
            ClearQueue();
            return;
        }

        Announcement an;
        an.text = _text;
        an.time = _time;
        announcementQueue.Enqueue(an);

        if (!showing)
        {
            transform.localScale = Vector3.one;
            StartCoroutine(StartAnnouncementAnim());
        }
    }

    private IEnumerator StartAnnouncementAnim()
    {
        if (announcementsLocked)
            yield return null;

        showing = true;
        float delay = 0.75f;

        SetAnnounceText(announcementQueue.Peek().text);
        LeanTween.move(gameObject, showingPosition, delay).setEase(LeanTweenType.easeInOutSine);

        yield return new WaitForSeconds(announcementQueue.Peek().time);
        LeanTween.move(gameObject, hiddenPosition, delay).setEase(LeanTweenType.easeInOutSine);

        yield return new WaitForSeconds(0.5f + delay);
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
