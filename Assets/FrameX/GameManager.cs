using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FrameX;

public enum CursorState
{ 
    Normal = 0,
    Handle = 1
}

public class GameManager : SingletonMono<GameManager>
{
    private void Start()
    {
        Init();
    }
    // ������Ϸִ��ʱ������Ҫִ�е�
    private void Init()
    {
        SetCursorState(CursorState.Normal);
    }
    #region ���ָ��
    private CursorState currentCursorState = CursorState.Normal;
    [SerializeField] Texture2D[] cursorTextures;
    public void SetCursorState(CursorState cursorState)
    {
        if (cursorState == currentCursorState) return;
        currentCursorState = cursorState;
        Texture2D tex = cursorTextures[(int)cursorState];
        Cursor.SetCursor(tex, Vector2.zero, CursorMode.Auto);
    }
    #endregion
}
