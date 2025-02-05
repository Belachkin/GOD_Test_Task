using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Button _fireButton;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _fire;
    
    [SerializeField] private TextMeshProUGUI _ammoText;
    
    private bool isFire = false;
    private void Start()
    {
        _fireButton.onClick.AddListener(Fire);
    }


    
    private void Fire()
    {
        if (isFire == false)
        {
            
            StartCoroutine(FireDelay());
            var bullet = Instantiate(_bulletPrefab, _firePoint.position, _firePoint.rotation);
            bullet.GetComponent<Bullet>().Direction = _firePoint.right;

            
            isFire = true;
        }
    }
    
    private IEnumerator FireDelay()
    {
        _fire.SetActive(true);
        
        yield return new WaitForSeconds(0.1f);
        
        _fire.SetActive(false);
        isFire = false;
    }
}
