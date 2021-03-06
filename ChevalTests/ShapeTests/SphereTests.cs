﻿using System;
using Cheval.DataStructure;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Templates;
using FluentAssertions;
using NUnit.Framework;

namespace ChevalTests.ShapeTests
{
    public class SphereTests
    {
        /*
         * Scenario: A sphere's default transformation
           Given s ← sphere()
           Then s.transform = identity_matrix
           */
        [Test]
        public void Sphere_default_translation_is_identity()
        {
            //Assign
            var s = new Sphere();
            //Assert
            s.Transform = Transform.IdentityMatrix;
        }

        /*
           Scenario: Changing a sphere's transformation
           Given s ← sphere()
           And t ← translation(2, 3, 4)
           When set_transform(s, t)
           Then s.transform = t
         */
        [Test]
        public void Changing_spheres_transform_test()
        {
            //Assign
            var s = new Sphere();
            var t = Transform.Translation(2, 3, 4);
            //Act
            s.Transform = t;
            //Assert
            s.Transform.Should().BeEquivalentTo(t);
        }

        /*
         * Scenario: The normal on a sphere at a point on the x axis
           Given s ← sphere()
           When n ← normal_at(s, point(1, 0, 0))
           Then n = vector(1, 0, 0)
           */
        [Test]
        public void Normal_on_sphere_at_point_on_x_axis()
        {
            //Assign
            var s = new Sphere();
            //Act
            var expected = ChevalTuple.Vector(1, 0, 0);
            var n = s.NormalAt(ChevalTuple.Point(1, 0, 0));
            //Assert
            n.Should().BeEquivalentTo(expected);
        }

        /*
          Scenario: The normal on a sphere at a point on the y axis
          Given s ← sphere()
          When n ← normal_at(s, point(0, 1, 0))
          Then n = vector(0, 1, 0)
       */
        [Test]
        public void Normal_on_sphere_at_point_on_y_axis()
        {
            //Assign
            var s = new Sphere();
            //Act
            var expected = ChevalTuple.Vector(0, 1, 0);
            var n = s.NormalAt(ChevalTuple.Point(0, 1, 0));
            //Assert
            n.Should().BeEquivalentTo(expected);
        }

        /*
       Scenario: The normal on a sphere at a point on the z axis
          Given s ← sphere()
          When n ← normal_at(s, point(0, 0, 1))
          Then n = vector(0, 0, 1)
          */
        [Test]
        public void Normal_on_sphere_at_point_on_z_axis()
        {
            //Assign
            var s = new Sphere();
            //Act
            var expected = ChevalTuple.Vector(0, 0, 1);
            var n = s.NormalAt(ChevalTuple.Point(0, 0, 1));
            //Assert
            n.Should().BeEquivalentTo(expected);
        }

        /*
          Scenario: The normal on a sphere at a nonaxial point
          Given s ← sphere()
          When n ← normal_at(s, point(√3/3, √3/3, √3/3))
          Then n = vector(√3/3, √3/3, √3/3)
          */
        [Test]
        public void Normal_on_sphere_at_point_on_nonaxial_point()
        {
            //Assign
            var s = new Sphere();
            //Act
            var expected = ChevalTuple.Vector(MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3);
            var n = s.NormalAt(ChevalTuple.Point(MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3));
            //Assert
            n.Should().BeEquivalentTo(expected);
        }

        /*
          Scenario: The normal is a normalized vector
          Given s ← sphere()
          When n ← normal_at(s, point(√3/3, √3/3, √3/3))
          Then n = normalize(n)
        */
        [Test]
        public void Normal_on_sphere_at_point_is_normaised_vector()
        {
            //Assign
            var s = new Sphere();
            //Act
            var n = s.NormalAt(ChevalTuple.Point(MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3, MathF.Sqrt(3) / 3));
            var result = ChevalTuple.Normalize(n);
            //Assert
            result.Should().BeEquivalentTo(n);
        }

        /*
         * Scenario: Computing the normal on a translated sphere
           Given s ← sphere()
           And set_transform(s, translation(0, 1, 0))
           When n ← normal_at(s, point(0, 1.70711, -0.70711))
           Then n = vector(0, 0.70711, -0.70711)
           */
        [Test]
        public void Computing_normal_with_translated_sphere()
        {
            //Assign
            var s = new Sphere();
            s.Transform = Transform.Translation(0, 1, 0);
            //Act
            var result = s.NormalAt(ChevalTuple.Point(0, 1.70711f, -0.70711f));
            var expected = ChevalTuple.Vector(0, 0.70711f, -0.70711f);
            //Assert
            result.Should().BeEquivalentTo(expected);
        }

        /*
           Scenario: Computing the normal on a transformed sphere
           Given s ← sphere()
           And m ← scaling(1, 0.5, 1) * rotation_z(π/5)
           And set_transform(s, m)
           When n ← normal_at(s, point(0, √2/2, -√2/2))
           Then n = vector(0, 0.97014, -0.24254)
         */
        [Test]
        public void Computing_normal_with_transformed_sphere()
        {
            //Assign
            var s = new Sphere();
            s.Transform = Transform.Scaling(1, 0.5f, 1) * Transform.RotationZ(MathF.PI / 5);
            //Act
            var result = s.NormalAt(ChevalTuple.Point(0, MathF.Sqrt(2) / 2, -MathF.Sqrt(2) / 2));
            var expected = ChevalTuple.Vector(0, 0.9701425f, -0.2425356f);

            //Assert
            result.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: A sphere has a default material
           Given s ← sphere()
           When m ← s.material
           Then m = material()

         */
        [Test]
        public void Sphere_has_default_material()
        {
            //Assign
            var s = new Sphere();
            //Act
            var m = s.Material;
            var expected = MaterialTemplate.Default;
            //Assert
            m.Should().BeEquivalentTo(expected);
        }
        /*
         * Scenario: A sphere may be assigned a material
           Given s ← sphere()
           And m ← material()
           And m.ambient ← 1
           When s.material ← m
           Then s.material = m
         */
        [Test]
        public void Sphere_may_be_assigned_material()
        {
            //Assign
            var s= new Sphere();
            var m = new Material
            {
                Ambient = 1
            };
            //Act
            s.Material = m;
            //Assert
            s.Material.Should().BeEquivalentTo(m);
        }
    }
}
