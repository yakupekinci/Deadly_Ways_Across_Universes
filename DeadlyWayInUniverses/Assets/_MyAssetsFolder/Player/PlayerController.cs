using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider ve Image için gerekli
using UnityTutorial.Manager;

namespace UnityTutorial.PlayerControl
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private float AnimBlendSpeed = 8.9f;
        [SerializeField] private Transform CameraRoot;
        [SerializeField] private Transform Camera;
        [SerializeField] private float UpperLimit = -40f;
        [SerializeField] private float BottomLimit = 70f;
        [SerializeField] private float MouseSensitivity = 21.9f;
        [SerializeField] private float Dis2Ground = 0.8f;
        [SerializeField] private LayerMask GroundCheck;
        [SerializeField] private float AirResistance = 0.8f;

        // Stamina
        [SerializeField] private float maxStamina = 100f;
        [SerializeField] private float staminaRegenRate = 5f;
        [SerializeField] private float runStaminaDrainRate = 10f;
        private float currentStamina;

        [SerializeField] private Slider staminaSlider; // UI Slider referansı

        // Health
        [SerializeField] private float maxHealth = 5f;
        private float currentHealth;
        [SerializeField] private Slider healthSlider; // UI Slider referansı
        [SerializeField] private GameObject gameOverPanel; // Game Over Paneli referansı
        [SerializeField] private Image bloodEffectImage; // Kan efekti Image referansı

        private Rigidbody _playerRigidbody;
        private InputManager _inputManager;
        private Animator _animator;
        private bool _grounded = false;
        private bool _hasAnimator;
        private int _xVelHash;
        private int _yVelHash;
        private int _groundHash;
        private int _fallingHash;
        private int _zVelHash;
        private float _xRotation;
        public bool CanMove;

        private const float _walkSpeed = 2f;
        private const float _runSpeed = 5f;
        private Vector2 _currentVelocity;

        private float lastDamageTime; // Son hasar alma zamanı
        private bool isRegeneratingHealth = false; // Sağlık yenileme durumu
        [SerializeField]
        private float stepInterval = 0.5f;
        private float lastStepTime;
        private void Start()
        {
            CanMove = true;
            _hasAnimator = TryGetComponent<Animator>(out _animator);
            _playerRigidbody = GetComponent<Rigidbody>();
            _inputManager = GetComponent<InputManager>();

            _xVelHash = Animator.StringToHash("X_Velocity");
            _yVelHash = Animator.StringToHash("Y_Velocity");
            _zVelHash = Animator.StringToHash("Z_Velocity");
            _groundHash = Animator.StringToHash("Grounded");
            _fallingHash = Animator.StringToHash("Falling");

            currentStamina = maxStamina; // Staminayı başlangıçta maksimum yapıyoruz
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;

            currentHealth = maxHealth; // Sağlığı başlangıçta maksimum yapıyoruz
            healthSlider.maxValue = maxHealth;
            healthSlider.value = currentHealth;
            gameOverPanel.SetActive(false); // Game Over panelini başlangıçta gizliyoruz

            // Kan efekti başlangıçta görünmez yapıyoruz
            Color bloodColor = bloodEffectImage.color;
            bloodColor.a = 0f;
            bloodEffectImage.color = bloodColor;
        }

        private void FixedUpdate()
        {
            if (!CanMove)
                return;

            SampleGround();
            Move();

            RegenerateStamina();
            UpdateStaminaUI();

            // Sağlık yenileme kontrolü
            if (Time.time - lastDamageTime >= 5f && !isRegeneratingHealth && currentHealth < maxHealth / 2)
            {
                StartCoroutine(RegenerateHealth());
            }
        }

        private void LateUpdate()
        {
            if (!CanMove)
                return;

            CamMovements();
        }

        private void Move()
        {
            if (!_hasAnimator) return;

            float targetSpeed = _inputManager.Run && currentStamina > 0 ? _runSpeed : _walkSpeed;
            if (_inputManager.Move == Vector2.zero) targetSpeed = 0;

            if (_grounded)
            {
                if (Time.time - lastStepTime > stepInterval && _inputManager.Run && _inputManager.Move != Vector2.zero)
                {
                    AudioManager.instance.PlayEffect("Run");
                    lastStepTime = Time.time;
                }
                if (Time.time - lastStepTime > stepInterval && _inputManager.Move != Vector2.zero)
                {
                    PlayStepSound();
                    lastStepTime = Time.time;
                }
                _currentVelocity.x = Mathf.Lerp(_currentVelocity.x, _inputManager.Move.x * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);
                _currentVelocity.y = Mathf.Lerp(_currentVelocity.y, _inputManager.Move.y * targetSpeed, AnimBlendSpeed * Time.fixedDeltaTime);

                var xVelDifference = _currentVelocity.x - _playerRigidbody.velocity.x;
                var zVelDifference = _currentVelocity.y - _playerRigidbody.velocity.z;

                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(xVelDifference, 0, zVelDifference)), ForceMode.VelocityChange);
            }
            else
            {
                _playerRigidbody.AddForce(transform.TransformVector(new Vector3(_currentVelocity.x * AirResistance, 0, _currentVelocity.y * AirResistance)), ForceMode.VelocityChange);
            }

            _animator.SetFloat(_xVelHash, _currentVelocity.x);
            _animator.SetFloat(_yVelHash, _currentVelocity.y);

            // Stamina'yı koşarken azalt
            if (_inputManager.Run && _inputManager.Move != Vector2.zero && currentStamina > 0)
            {
                currentStamina -= runStaminaDrainRate * Time.fixedDeltaTime;
                if (currentStamina < 0)
                {
                    currentStamina = 0;
                }
            }
        }
        private void PlayStepSound()
        {
            UnityEngine.SceneManagement.Scene activeScene = SceneManager.GetActiveScene();
            switch (activeScene.buildIndex)
            {
                case 1:
                    AudioManager.instance.PlayEffect("Step1");

                    break;
                case 2:
                    AudioManager.instance.PlayEffect("Step2");
                    break;
                case 3:
                    AudioManager.instance.PlayEffect("Step4");
                    break;
                default:
                    Debug.LogWarning("Unknown scene index: " + activeScene.buildIndex);
                    break;
            }
        }
        private void RegenerateStamina()
        {
            if (!_inputManager.Run && currentStamina < maxStamina)
            {
                currentStamina += staminaRegenRate * Time.fixedDeltaTime;
                if (currentStamina > maxStamina)
                {
                    currentStamina = maxStamina;
                }
            }
        }

        private void UpdateStaminaUI()
        {
            staminaSlider.value = currentStamina;
        }

        private void CamMovements()
        {
            if (!_hasAnimator) return;

            var Mouse_X = _inputManager.Look.x;
            var Mouse_Y = _inputManager.Look.y;
            Camera.position = CameraRoot.position;

            _xRotation -= Mouse_Y * MouseSensitivity * Time.smoothDeltaTime;
            _xRotation = Mathf.Clamp(_xRotation, UpperLimit, BottomLimit);

            Camera.localRotation = Quaternion.Euler(_xRotation, 0, 0);
            _playerRigidbody.MoveRotation(_playerRigidbody.rotation * Quaternion.Euler(0, Mouse_X * MouseSensitivity * Time.smoothDeltaTime, 0));
        }

        private void SampleGround()
        {
            if (!_hasAnimator) return;

            RaycastHit hitInfo;
            if (Physics.Raycast(_playerRigidbody.worldCenterOfMass, Vector3.down, out hitInfo, Dis2Ground + 0.1f, GroundCheck))
            {
                // Grounded
                _grounded = true;
                SetAnimationGrounding();

                return;
            }
            // Falling
            _grounded = false;
            _animator.SetFloat(_zVelHash, _playerRigidbody.velocity.y);
            SetAnimationGrounding();
            return;
        }

        private void SetAnimationGrounding()
        {
            _animator.SetBool(_fallingHash, !_grounded);
            _animator.SetBool(_groundHash, _grounded);
        }

        public void PauseAnimations()
        {
            if (_hasAnimator)
            {
                _animator.speed = 0f; // Tüm animasyonları duraklat
            }
        }

        public void ResumeAnimations()
        {
            if (_hasAnimator)
            {
                _animator.speed = 1f; // Tüm animasyonları devam ettir
            }
        }
        [SerializeField] private Image bloodEffect;

        // Zombinin oyuncuya vurduğunda bu fonksiyonu çağırın
        public void TriggerBloodEffect()
        {
            StartCoroutine(ShowBloodEffect());
        }

        private IEnumerator ShowBloodEffect()
        {
            bloodEffect.gameObject.SetActive(true);
            yield return new WaitForSeconds(1f);
            bloodEffect.gameObject.SetActive(false);
        }
        public void TakeDamage(float damage)
        {
            AudioManager.instance.PlayEffect("Damage");
            currentHealth -= damage;
            healthSlider.value = currentHealth;
            lastDamageTime = Time.time; // Son hasar alma zamanını güncelle
            isRegeneratingHealth = false; // Sağlık yenilemeyi durdur

            // Kan efekti alfa değerini güncelle
            UpdateBloodEffectAlpha();

            if (currentHealth <= 0)
            {
                GameOver();
            }
        }


        private void UpdateBloodEffectAlpha()
        {
            float healthPercentage = currentHealth / maxHealth;
            Color bloodColor = bloodEffectImage.color;
            if (healthPercentage < 0.5f)
            {
                bloodColor.a = 1f - (healthPercentage * 2); // Sağlık %50'nin altına düştüğünde kan efekti daha belirgin olur
            }
            else
            {
                bloodColor.a = 0f; // Sağlık %50'nin üzerindeyse kan efekti görünmez
            }
            bloodEffectImage.color = bloodColor;
        }

        private IEnumerator RegenerateHealth()
        {
            isRegeneratingHealth = true;
            float targetHealth = maxHealth / 2;
            float regenSpeed = 0.4f; // Sağlık yenileme hızı

            while (currentHealth < targetHealth && isRegeneratingHealth)
            {
                currentHealth += regenSpeed * Time.deltaTime;
                healthSlider.value = currentHealth;
                UpdateBloodEffectAlpha();

                yield return null;
            }

            isRegeneratingHealth = false;
        }


        private void GameOver()
        {
            CanMove = false;
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Zamanı durdur
            Cursor.lockState = CursorLockMode.None; // Kursörü serbest bırak
            Cursor.visible = true; // Kursörü görünür yap
            // Diğer game over işlemleri
        }

    }
}
