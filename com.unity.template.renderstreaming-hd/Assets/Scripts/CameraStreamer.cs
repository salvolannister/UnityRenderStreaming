using Unity.WebRTC;
using UnityEngine;

namespace Unity.RenderStreaming
{
    [RequireComponent(typeof(Camera))]
    public class CameraStreamer : VideoStreamBase
    {
        private Camera m_camera;

        void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        void OnEnable()
        {
            // todo(kazuki): remove bitrate parameter because it is not supported
            m_track = m_camera.CaptureStreamTrack(streamingSize.x, streamingSize.y, 1000000);
            RenderStreaming.Instance?.AddVideoStreamTrack(m_track);

            if (localImage != null)
            {
                localImage.texture = m_camera.targetTexture;
            }

            OnEnableComplete?.Invoke();
        }

        void OnDisable()
        {
            RenderStreaming.Instance?.RemoveVideoStreamTrack(m_track);


            if (localImage != null)
            {
                localImage.texture = null;
            }
        }
    }
}
