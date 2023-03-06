using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public static partial class GFunc
{
    // 카메라 사이즈를 리턴 하는 함수
    public static Vector2 GetCameraSize()
    {
        Vector2 CameraSize = Vector2.zero;
        CameraSize.y = Camera.main.orthographicSize * 2.0f;
        CameraSize.x = CameraSize.y * Camera.main.aspect;

        return CameraSize;

    }
    //! �ؽ�Ʈ�޽����� ������ ������Ʈ�� �ؽ�Ʈ�� �����ϴ� �Լ�
    public static void SetTmpText(this GameObject obj_, string text_)
    {
        TMP_Text tmpTxt = obj_.GetComponent<TMP_Text>();
        if (tmpTxt == null || tmpTxt == default(TMP_Text))
        {
            return;
        }       // if: ������ �ؽ�Ʈ�޽� ������Ʈ�� ���� ���

        // ������ �ؽ�Ʈ�޽� ������Ʈ�� �����ϴ� ���
        tmpTxt.text = text_;
    }       // SetTextMeshPro()
}
