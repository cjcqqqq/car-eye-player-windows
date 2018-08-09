namespace CarEyePlayerDemo.Player
{
	internal enum CE_PARAM_ID
	{
		PARAM_MEDIA_DURATION = 0x1000,
		PARAM_MEDIA_POSITION,

		// media detail info
		PARAM_VIDEO_WIDTH,
		PARAM_VIDEO_HEIGHT,

		// video display mode
		PARAM_VIDEO_MODE,

		// audio volume control
		PARAM_AUDIO_VOLUME,

		// playback speed control
		PARAM_PLAY_SPEED_VALUE,
		PARAM_PLAY_SPEED_TYPE,

		// visual effect mode
		PARAM_VISUAL_EFFECT,

		// audio/video sync diff
		PARAM_AVSYNC_TIME_DIFF,

		// get player init params
		PARAM_PLAYER_INIT_PARAMS,
		//-- public

		//++ for adev
		PARAM_ADEV_GET_CONTEXT = 0x2000,
		//-- for adev

		//++ for vdev
		PARAM_VDEV_GET_CONTEXT = 0x3000,
		PARAM_VDEV_POST_SURFACE,
		PARAM_VDEV_GET_D3DDEV,
		PARAM_VDEV_D3D_ROTATE,
		//-- for vdev

		//++ for render
		PARAM_RENDER_GET_CONTEXT = 0x4000,
		PARAM_RENDER_STEPFORWARD,
	}
}
