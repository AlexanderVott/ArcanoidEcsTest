using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RedDev.Game {
    sealed class InputProcessing : IEcsInitSystem, IEcsRunSystem
	{
	    private readonly EcsFilter<InputAxis> _inputFilter = null;

	    private ArcanoidInput _input;

		public void Init()
		{
			//_input.Player.SetCallbacks(this);
			/*_input.Player.Move.started += OnMove;
			_input.Player.Move.performed += OnMove;
			_input.Player.Move.canceled += OnMove;*/
		}

        public void OnMove(InputAction.CallbackContext context)
        {
	        var move = context.ReadValue<Vector2>();
			foreach (var item in _inputFilter)
			{
				ref var axis = ref _inputFilter.Get1(item);
				axis.axis = move;
			}
		}

        public void Run()
        {
	        var move = _input.Player.Move.ReadValue<Vector2>();
			foreach (var item in _inputFilter)
			{
				ref var axis = ref _inputFilter.Get1(item);
				axis.axis = move;
			}
		}
	}
}