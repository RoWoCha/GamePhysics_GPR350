using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    enum ProjectileType
    {
        SPRING,
        ROD,
        NUM_PROJECTILE_TYPES
    }
    
    [Header("Player Settings")]
    public float playerRotationSpeed = 60;

    [Header("Spring Weapon Settings")]
    public float spring_mass_one;
    public float spring_mass_two;
    public float spring_volume_one;
    public float spring_volume_two;
    public float spring_height_one;
    public float spring_height_two;
    public float spring_speed_one;
    public float spring_speed_two;
    public Vector2 spring_gravity;
    public float spring_damping_one;
    public float spring_damping_two;
    public Sprite spring_sprite_one;
    public Sprite spring_sprite_two;
    public Color spring_color_one;
    public Color spring_color_two;
    public float spring_constant;
    public float spring_rest_length;

    [Header("Rod Weapon Settings")]
    public float rod_mass_one;
    public float rod_mass_two;
    public float rod_volume_one;
    public float rod_volume_two;
    public float rod_height_one;
    public float rod_height_two;
    public float rod_speed_one;
    public float rod_speed_two;
    public Vector2 rod_gravity;
    public float rod_damping_one;
    public float rod_damping_two;
    public Sprite rod_sprite_one;
    public Sprite rod_sprite_two;
    public Color rod_color_one;
    public Color rod_color_two;
    public float rod_length;

    Transform bulletSpawnTransform;
    ProjectileType currentWeapon;

    void Start()
    {
        bulletSpawnTransform = transform.Find("BulletSpawn");
    }

    void Update()
    {
        CheckInput();
    }

    void CheckInput()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            transform.Rotate(new Vector3(0, 0, playerRotationSpeed * Time.deltaTime));
        }
        else if (Input.GetKey(KeyCode.Alpha2))
        {
            transform.Rotate(new Vector3(0, 0, -playerRotationSpeed * Time.deltaTime));
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            ChangeProjectileType();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShootProjectile();
        }
    }

    void ChangeProjectileType()
    {
        currentWeapon++;
        if ((int)currentWeapon >= (int)ProjectileType.NUM_PROJECTILE_TYPES)
            currentWeapon = 0;
    }

    void ShootProjectile()
    {
        if (currentWeapon == ProjectileType.SPRING)
        {
            GameObject object1 = new GameObject("SpringProjectile");
            object1.transform.position = bulletSpawnTransform.position;
            object1.transform.rotation = bulletSpawnTransform.rotation;
            object1.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            object1.tag = "Projectiles";
            object1.AddComponent<Particle2D>();
            object1.AddComponent<SpriteRenderer>();
            object1.GetComponent<SpriteRenderer>().sprite = spring_sprite_one;
            object1.GetComponent<SpriteRenderer>().color = spring_color_one;
            object1.GetComponent<Particle2D>().Init(spring_mass_one, spring_volume_one, spring_height_one,
                transform.up * spring_speed_one, spring_gravity, spring_damping_one, false);
            Integrator.instance.particlesList.Add(object1.GetComponent<Particle2D>());

            GameObject object2 = new GameObject("SpringProjectile");
            object2.transform.position = bulletSpawnTransform.position;
            object2.transform.rotation = bulletSpawnTransform.rotation;
            object2.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            object2.tag = "Projectiles";
            object2.AddComponent<Particle2D>();
            object2.AddComponent<SpriteRenderer>();
            object2.GetComponent<SpriteRenderer>().sprite = spring_sprite_two;
            object2.GetComponent<SpriteRenderer>().color = spring_color_two;
            object2.GetComponent<Particle2D>().Init(spring_mass_two, spring_volume_two, spring_height_two,
                transform.up * spring_speed_two, spring_gravity, spring_damping_two, false);
            Integrator.instance.particlesList.Add(object2.GetComponent<Particle2D>());

            ForceManager.instance.AddSpringForceGenerator(object1, object2, spring_constant, spring_rest_length);
        }
        else if (currentWeapon == ProjectileType.ROD)
        {
            GameObject object1 = new GameObject("RodProjectile");
            object1.transform.position = bulletSpawnTransform.position;
            object1.transform.rotation = bulletSpawnTransform.rotation;
            object1.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            object1.tag = "Projectiles";
            object1.AddComponent<Particle2D>();
            object1.AddComponent<SpriteRenderer>();
            object1.GetComponent<SpriteRenderer>().sprite = rod_sprite_one;
            object1.GetComponent<SpriteRenderer>().color = rod_color_one;
            object1.GetComponent<Particle2D>().Init(rod_mass_one, rod_volume_one, rod_height_one,
                transform.up * rod_speed_one, rod_gravity, rod_damping_one, false);
            Integrator.instance.particlesList.Add(object1.GetComponent<Particle2D>());

            GameObject object2 = new GameObject("RodProjectile");
            object2.transform.position = bulletSpawnTransform.position;
            object2.transform.rotation = bulletSpawnTransform.rotation;
            object2.transform.localScale = new Vector3(0.5f, 0.5f, 1f);
            object2.tag = "Projectiles";
            object2.AddComponent<Particle2D>();
            object2.AddComponent<SpriteRenderer>();
            object2.GetComponent<SpriteRenderer>().sprite = rod_sprite_two;
            object2.GetComponent<SpriteRenderer>().color = rod_color_two;
            object2.GetComponent<Particle2D>().Init(rod_mass_two, rod_volume_two, rod_height_two,
                transform.up * rod_speed_two, rod_gravity, rod_damping_two, false);
            Integrator.instance.particlesList.Add(object2.GetComponent<Particle2D>());

            GameObject linkObj = new GameObject("RodLink");
            linkObj.AddComponent<Particle2DRod>();
            linkObj.GetComponent<Particle2DRod>().Init(object1, object2, rod_length);
            ContactResolver.instance.particleLinks.Add(linkObj.GetComponent<Particle2DRod>());
        }
    }
}
