using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class GFunc
{
    public static GameObject FindChildObj(
        this GameObject targetObj_, string objName_)
    {
        GameObject searchResult = default;
        GameObject searchTarget = default;
        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            if (searchTarget.name.Equals(objName_))
            {
                searchResult = searchTarget;
                return searchResult;
            }
            else
            {
                searchResult = FindChildObj(searchTarget, objName_);

                //       
                if (searchResult == null || searchResult == default) { /* Pass */ }
                else { return searchResult; }
            }
        }       // loop

        return searchResult;
    }
    // 특정 오브젝트의 자식 오브젝트를 서치해서 컴포넌트를 찾아주는 함수
    public static T FindChildComponent<T>(
        this GameObject targetObj_, string objName_) where T : Component
    {
        T searchResultComponent = default(T);
        GameObject searchResultObj = default(GameObject);

        searchResultObj = targetObj_.FindChildObj(objName_);
        if (searchResultObj.IsValid())
        {
            searchResultComponent = searchResultObj.GetComponent<T>();
        }

        return searchResultComponent;
    }       // FindChildObj()


    //! ���� ��Ʈ ������Ʈ�� ��ġ�ؼ� ã���ִ� �Լ�
    public static GameObject GetRootObj(string objName_)
    {
        Scene activeScene_ = GetActiveScene();
        GameObject[] rootObjs_ = activeScene_.GetRootGameObjects();

        GameObject targetObj_ = default;
        foreach (GameObject rootObj in rootObjs_)
        {
            if (rootObj.name.Equals(objName_))
            {
                targetObj_ = rootObj;
                return targetObj_;
            }
            else { continue; }
        }       // loop

        return targetObj_;
    }       // GetRootObj()



    // 특정 오브젝트의 자식 오브젝트를 모두 리턴하는 함수
    public static List<GameObject> GetChildrenObjs(this GameObject targetObj_)
    {
        List<GameObject> objs = new List<GameObject>();
        GameObject searchTarget = default;

        for (int i = 0; i < targetObj_.transform.childCount; i++)
        {
            searchTarget = targetObj_.transform.GetChild(i).gameObject;
            objs.Add(searchTarget);
        }

        if (objs.IsValid()) { return objs; }
        else { return default(List<GameObject>); }
    } // GetChildrenObjs()



    //! RectTransform �� ã�Ƽ� �����ϴ� �Լ�
    public static RectTransform GetRect(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>();
    }       // GetRect()

    //! RectTransform ���� sizeDelta�� ã�Ƽ� �����ϴ� �Լ�
    public static Vector2 GetRectSizeDelta(this GameObject obj_)
    {
        return obj_.GetComponentMust<RectTransform>().sizeDelta;
    }       // GetRectSizeDelta()

    //! ���� Ȱ��ȭ �Ǿ� �ִ� ���� ã���ִ� �Լ�
    public static Scene GetActiveScene()
    {
        Scene activeScene_ = SceneManager.GetActiveScene();
        return activeScene_;
    }       // GetActiveScene()

    #region  objext transform control
    // 오브젝트의 로컬 스케일을 변경하는 함수
    public static void SetLocalScale(this GameObject obj_, Vector3 localScale_)
    {
        obj_.transform.localScale = localScale_;
    }
    // SetLocalPos()

    // 오브젝트의 로컬 포지션을 변경하는 함수
    public static void SetLocalPos(this GameObject obj_,
        float x, float y, float z)
    {
        obj_.transform.localPosition = new Vector3(x, y, z);
    }       // SetLocalPos()

    // 오브젝트의 로컬 포지션을 변경하는 함수
    public static void SetLocalPos(this GameObject obj_,
      Vector3 localPos)
    {
        obj_.transform.localPosition = localPos;
    }

    //! ������Ʈ�� ���� �������� �����ϴ� �Լ�
    public static void AddLocalPos(this GameObject obj_,
        float x, float y, float z)
    {
        obj_.transform.localPosition =
            obj_.transform.localPosition + new Vector3(x, y, z);
    }       // AddLocalPos()

    //! ������Ʈ�� ��Ŀ �������� �����ϴ� �Լ�
    public static void AddAnchoredPos(this GameObject obj_,
        float x, float y)
    {
        obj_.GetRect().anchoredPosition += new Vector2(x, y);
    }       // AddAnchoredPos()

    //! ������Ʈ�� ��Ŀ �������� �����ϴ� �Լ�
    public static void AddAnchoredPos(this GameObject obj_,
        Vector2 position2D)
    {
        obj_.GetRect().anchoredPosition += position2D;
    }       // AddAnchoredPos()

    //! Ʈ�������� ����ؼ� ������Ʈ�� �����̴� �Լ�
    public static void Translate(this Transform transform_, Vector2 moveVector)
    {
        transform_.Translate(moveVector.x, moveVector.y, 0f);
    }       // Translate()

    //! ������Ʈ �������� �Լ�
    public static T GetComponentMust<T>(this GameObject obj) where T : Component
    {
        T component_ = obj.GetComponent<T>();

        GFunc.Assert(component_.IsValid<T>() != false,
            string.Format("{0}���� {1}��(��) ã�� �� �����ϴ�.",
            obj.name, component_.GetType().Name));

        return component_;
    }       // GetComponentMust()

    //! ���ο� ������Ʈ�� ���� ������Ʈ�� �����ϴ� �Լ�
    public static T CreateObj<T>(string objName) where T : Component
    {
        GameObject newObj = new GameObject(objName);
        return newObj.AddComponent<T>();
    }       // CreateObj()


    // 오브젝트를 파괴하는 함수
    public static void DestroyObj(this GameObject obj_, float delay = 0.0f)
    {
        Object.Destroy(obj_, delay);
    }

    // 로컬 포지션을 기준으로 두 타일 오브젝트의 위치를 비교하는 함수
    public static int compareTileObjToLocalPos2D(GameObject firstObj, GameObject secondObj)
    {
        Vector2 fPos = firstObj.transform.localPosition;
        Vector2 sPos = secondObj.transform.localPosition;

        int compareResult = 0;
        if (fPos.y.IsEquals(sPos.y))
        {
            // x 포지션이 같으면 같은 타일이므로 0을 리턴
            if (fPos.x.IsEquals(sPos.x)) { compareResult = 0; }
            else
            {
                if (fPos.x < sPos.x) { compareResult = -1; }
                else { compareResult = 1; }
            } // else: x 포지션이 다른 경우 대소비교
            return compareResult;
        } // if: y포지션이 같은 경우

        // y 포지션이 다른 경우 대소비교
        if (fPos.y < sPos.y) { compareResult = -1; }
        else { compareResult = 1; }

        return compareResult;
    } //compareTileObjToLocalPos2D()
    #endregion
}
