using Spine.Unity; // Import Spine.Unity để làm việc với SkeletonGraphic
using UnityEngine;

public class SpineUIAnimationController : MonoBehaviour
{
    public SkeletonGraphic skeletonGraphic; // Đối tượng SkeletonGraphic trong UI

    void Start()
    {
        // Chạy hoạt ảnh mặc định khi bắt đầu
        PlayAnimation("1Star");
    }

    public void PlayAnimation(string animationName)
    {
        if (skeletonGraphic == null)
        {
            Debug.LogError("SkeletonGraphic is not assigned!");
            return;
        }

        // Phát hoạt ảnh theo tên
        skeletonGraphic.AnimationState.SetAnimation(0, animationName, false); // `false` để không lặp
    }

    // Test các hoạt ảnh thông qua phím bấm (hoặc gọi từ nút UI)

}
    