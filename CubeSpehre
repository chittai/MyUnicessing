using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unicessing;


public class CubeParticle : UGraphics {

    ArrayList particles;
    int MAX_PARTICLES = 100;
    public static float MAX_SPEED = 4.0f;
    public static float SHRINK_RATE = 0.15f;
    int boxsize = 250;
    bool moveflag = false;


    protected override void Setup()
        {
        // P2D or P3D : Processing Coordinate System x=1, y=-1, z=-1
        size(800, 600, P3D, 0.01f);
        //background(0);
        noFill();
        particles = new ArrayList();

        for (int i = 0; i < MAX_PARTICLES; i++)
        {
            Vector3 location = new Vector3(random(-boxsize / 2, boxsize / 2), random(-boxsize / 2, boxsize / 2), random(-boxsize / 2, boxsize / 2));
            particles.Add(new Particle(location));
        }
    }

    protected override void Draw()
        {
        //background(0);
        //translate(widtsh / 2, height / 2);
        rotateY((float)(Time.frameCount)/100f);

        stroke(255);
        noFill();
        box(boxsize);

        if (Input.GetMouseButtonDown(0)){
            moveflag = true;
         }

        for (int i = 0; i < particles.Count; i++)
        {
            Particle _particle = (Particle)particles[i];
            if (_particle.isDead())
            {
            }
            else
            {
                fill(_particle.c);
                noStroke();
                pushMatrix();
                translate(_particle.pos.x, _particle.pos.y, _particle.pos.z);
                sphere(_particle.size);
                popMatrix();

                if (moveflag == true)
                {
                    _particle.move();
                }
            }
        }
    }
}

public class Particle : USubGraphics
{
    static UMath um = new UMath();
    static UConstants uc = new UConstants();
    Vector3 velocity = new Vector3(um.random(-CubeParticle.MAX_SPEED, CubeParticle.MAX_SPEED), um.random(-CubeParticle.MAX_SPEED, CubeParticle.MAX_SPEED));
    public Vector3 pos;
    public float size = um.random(20);
    public Color c = new Color(Random.value, Random.value, 1.0f, 0.5f);

    public bool _isDead;

    public Particle(Vector3 origin)
    {
        pos = origin;
        _isDead = false;
        size = um.random(20);
        c = new Color(Random.value, Random.value, 1.0f, 0.5f);
}

    public void draw()
    {

       //g.fill(c);
       //g.noStroke();
       //g.pushMatrix();
       //g.translate(pos.x, pos.y, pos.z);
       ////ellipse(pos.x,pos.y,size,size);
       //g.sphere(size);
       //g.popMatrix();
    }

    public void move()
    {
        pos += velocity;
        size -= CubeParticle.SHRINK_RATE;
    }

    public bool isDead()
    {
        if (size <= 0)
        {
            _isDead = true;
            return true;
        }
        else
        {
            _isDead = false;
            return false;
        }
    }
}
