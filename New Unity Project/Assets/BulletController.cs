using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject Fx;

    private void OnCollisionEnter(Collision collision)
    {
        // 현재 스크립트가 포함된 gameObject를 삭제
        Destroy(this.gameObject, 0.05f);

        // Instantiate = 복제 함수
        // Fx의 복사본을 Ob로 넘겨줌
        GameObject Obj = Instantiate(Fx);

        // 현재 총알 = 이 스크립트가 포함된 gameObject
        // 총알의 진행방향의 반대방향을 바라보는 벡터를 구함
        Vector3 Direction = (transform.position - collision.transform.position).normalized;

        // 현재 총알의 위치로부터 Direction 방향으로 2.0 만큼 이동
        // 결과적으로 총알 진행방향의 반대로 물러남
        Obj.transform.position = transform.position + (Direction * 2.0f);

        // Fx의 복사본인 Obj를 애니메이션이 끝나는 1.5초 뒤에 삭제
        Destroy(Obj.gameObject, 1.5f);
    }
}
