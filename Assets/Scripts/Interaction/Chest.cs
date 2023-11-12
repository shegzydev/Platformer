using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public class Chest : InteractibleBase
{
    Animator animator;
    [SerializeField] UnityEvent onChestOpen;
    bool opened;

    public override void Interact()
    {
        base.Interact();
        if (opened) return;
        animator = GetComponent<Animator>();
        Open();
    }

    void Open()
    {
        opened = true;
        GetComponent<Animator>().Play("Open");
        StartCoroutine(OpenChest());
    }

    IEnumerator OpenChest()
    {
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).IsName("Open"));
        yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.95f);
        onChestOpen?.Invoke();
    }
}
