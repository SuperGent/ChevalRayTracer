﻿using System;
using System.Collections.Generic;
using System.Text;
using Cheval.Helper;
using Cheval.Models;
using Cheval.Models.Shapes;
using Cheval.Patterns;
using Cheval.Templates;
using FluentAssertions;
using NUnit.Framework;
using static Cheval.DataStructure.ChevalTuple;
using static Cheval.Models.ChevalColour;
using static Cheval.Templates.ColourTemplate;

namespace ChevalTests.PatternTests
{
    public class PatternTests
    {
        /*
         * Scenario: Creating a stripe pattern
           Given pattern ← stripe_pattern(white, black)
           Then pattern.a = white
           And pattern.b = black
         */
        [Test]
        public void Stripe_pattern_test()
        {
            //Assign
            var pattern = new Stripe(Black, ColourTemplate.White);
            //Assert
            pattern.Colours[0].Should().BeEquivalentTo(Black);
            pattern.Colours[1].Should().BeEquivalentTo(ColourTemplate.White);
        }
        /*
         * Scenario: A stripe pattern is constant in y
           Given pattern ← stripe_pattern(white, black)
           Then stripe_at(pattern, point(0, 0, 0)) = white
           And stripe_at(pattern, point(0, 1, 0)) = white
           And stripe_at(pattern, point(0, 2, 0)) = white
           */
        [Test]
        public void Stripe_pattern_is_constant_in_Y()
        {
            //Assign
            var pattern = new Stripe(White, Black);
            //Act
            var result1 = pattern.ColourAt(Point(0, 0, 0));
            var result2 = pattern.ColourAt(Point(0, 1, 0));
            var result3 = pattern.ColourAt(Point(0, 2, 0));
            //Assert
            result1.Should().BeEquivalentTo(White);
            result2.Should().BeEquivalentTo(White);
            result3.Should().BeEquivalentTo(White);

        }
        /*

           Scenario: A stripe pattern is constant in z
           Given pattern ← stripe_pattern(white, black)
           Then stripe_at(pattern, point(0, 0, 0)) = white
           And stripe_at(pattern, point(0, 0, 1)) = white
           And stripe_at(pattern, point(0, 0, 2)) = white
           */
        [Test]
        public void Stripe_pattern_is_constant_in_Z()
        {
            //Assign
            var pattern = new Stripe(White, Black);
            //Act
            var result1 = pattern.ColourAt(Point(0, 0, 0));
            var result2 = pattern.ColourAt(Point(0, 0, 1));
            var result3 = pattern.ColourAt(Point(0, 0, 2));
            //Assert
            result1.Should().BeEquivalentTo(White);
            result2.Should().BeEquivalentTo(White);
            result3.Should().BeEquivalentTo(White);
        }
        /*
           Scenario: A stripe pattern alternates in x
           Given pattern ← stripe_pattern(white, black)
           Then stripe_at(pattern, point(0, 0, 0)) = white
           And stripe_at(pattern, point(0.9, 0, 0)) = white
           And stripe_at(pattern, point(1, 0, 0)) = black
           And stripe_at(pattern, point(-0.1, 0, 0)) = black
           And stripe_at(pattern, point(-1, 0, 0)) = black
           And stripe_at(pattern, point(-1.1, 0, 0)) = white
         */
        [Test]
        public void Stripe_pattern_alternates_in_X()
        {
            //Assign
            var pattern = new Stripe(White, Black);
            //Act
            var result1 = pattern.ColourAt(Point(0, 0, 0));
            var result2 = pattern.ColourAt(Point(0.9, 0, 0));
            var result3 = pattern.ColourAt(Point(1, 0, 0));
            var result4 = pattern.ColourAt(Point(-0.1, 0, 0));
            var result5 = pattern.ColourAt(Point(-1, 0, 0));
            var result6 = pattern.ColourAt(Point(-1.1, 0, 0));
            //Assert
            result1.Should().BeEquivalentTo(White);
            result2.Should().BeEquivalentTo(White);
            result3.Should().BeEquivalentTo(Black);
            result4.Should().BeEquivalentTo(Black);
            result5.Should().BeEquivalentTo(Black);
            result6.Should().BeEquivalentTo(White);
        }

        /*
         * Scenario: Stripes with an object transformation
           Given object ← sphere()
           And set_transform(object, scaling(2, 2, 2))
           And pattern ← stripe_pattern(white, black)
           When c ← stripe_at_object(pattern, object, point(1.5, 0, 0))
           Then c = white
           */
        [Test]
        public void Stripes_with_Object_transformation()
        {
            //Assign
            var shape = new Sphere();
            shape.Transform = Transform.Scaling(2, 2, 2);
            var pattern = new Stripe(White, Black);
            //Act
            ChevalColour col = pattern.ColourAtObject(shape, Point(1.5, 0, 0));
            //Assert
            col.Should().BeEquivalentTo(White);
        }
        /*
           Scenario: Stripes with a pattern transformation
           Given object ← sphere()
           And pattern ← stripe_pattern(white, black)
           And set_pattern_transform(pattern, scaling(2, 2, 2))
           When c ← stripe_at_object(pattern, object, point(1.5, 0, 0))
           Then c = white
           */
        [Test]
        public void Stripes_with_pattern_transformation()
        {
            //Assign
            var shape = new Sphere();
            var pattern = new Stripe(White, Black);
            pattern.Transform = Transform.Scaling(2, 2, 2);
            //Act
            ChevalColour col = pattern.ColourAtObject(shape, Point(1.5, 0, 0));
            //Assert
            col.Should().BeEquivalentTo(White);
        }
        /*
           Scenario: Stripes with both an object and a pattern transformation
           Given object ← sphere()
           And set_transform(object, scaling(2, 2, 2))
           And pattern ← stripe_pattern(white, black)
           And set_pattern_transform(pattern, translation(0.5, 0, 0))
           When c ← stripe_at_object(pattern, object, point(2.5, 0, 0))
           Then c = white
         */
        [Test]
        public void Stripes_with_pattern_transformation_and_object_transformation()
        {
            //Assign
            var shape = new Sphere();
            shape.Transform = Transform.Scaling(2, 2, 2);
            var pattern = new Stripe(White, Black);
            pattern.Transform = Transform.Translation(0.5, 0, 0);
            //Act
            ChevalColour col = pattern.ColourAtObject(shape, Point(2.5, 0, 0));
            //Assert
            col.Should().BeEquivalentTo(White);
        }
        /*
         * Scenario: A gradient linearly interpolates between colors
           Given pattern ← gradient_pattern(white, black)
           Then pattern_at(pattern, point(0, 0, 0)) = white
           And pattern_at(pattern, point(0.25, 0, 0)) = color(0.75, 0.75, 0.75)
           And pattern_at(pattern, point(0.5, 0, 0)) = color(0.5, 0.5, 0.5)
           And pattern_at(pattern, point(0.75, 0, 0)) = color(0.25, 0.25, 0.25)
         */
        [Test]
        public void Gradient_linearly_interpolates_between_colours()
        {
            //Assign
            var pattern = new Gradient(White,Black);
            //Act
            var result1 = pattern.ColourAt(Point(0, 0, 0));
            var expected1 = White;
            var result2 = pattern.ColourAt(Point(0.25, 0, 0));
            var expected2 = new ChevalColour(0.75,0.75,0.75);
            var result3 = pattern.ColourAt(Point(0.5, 0, 0));
            var expected3 = new ChevalColour(0.5, 0.5, 0.5);
            var result4 = pattern.ColourAt(Point(0.75, 0, 0));
            var expected4 = new ChevalColour(0.25, 0.25, 0.25);
            var result5 = pattern.ColourAt(Point(1, 0, 0));
            var expected5 = White;
            //Assert
            result1.Should().BeEquivalentTo(expected1);
            result2.Should().BeEquivalentTo(expected2);
            result3.Should().BeEquivalentTo(expected3);
            result4.Should().BeEquivalentTo(expected4);
            result5.Should().BeEquivalentTo(expected5);
        }
        /*
         * Scenario: A ring should extend in both x and z
           Given pattern ← ring_pattern(white, black)
           Then pattern_at(pattern, point(0, 0, 0)) = white
           And pattern_at(pattern, point(1, 0, 0)) = black
           And pattern_at(pattern, point(0, 0, 1)) = black
           # 0.708 = just slightly more than √2/2
           And pattern_at(pattern, point(0.708, 0, 0.708)) = black
         */
        [Test]
        public void Ring_should_extend_in_x_z()
        {
            //Assign
            var pattern = new Ring(White, Black);
            //Assert
            pattern.ColourAt(Point(0,0,0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(1, 0, 0)).Should().BeEquivalentTo(Black);
            pattern.ColourAt(Point(0, 0, 1)).Should().BeEquivalentTo(Black);
            pattern.ColourAt(Point(0.708, 0, 0.708)).Should().BeEquivalentTo(Black);
            pattern.ColourAt(Point(2, 0, 0)).Should().BeEquivalentTo(White);
        }

        [Test]
        public void Ring_should_extend_in_x_z_3_colours()
        {
            //Assign
            var pattern = new Ring(White, Black);
            var red = new ChevalColour(1, 0, 0);
            pattern.Colours.Add(red);
            //Assert
            pattern.ColourAt(Point(0, 0, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(1, 0, 0)).Should().BeEquivalentTo(Black);
            pattern.ColourAt(Point(2, 0, 0)).Should().BeEquivalentTo(red);
            pattern.ColourAt(Point(3, 0, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(4, 0, 0)).Should().BeEquivalentTo(Black);
        }

        /*
         * Scenario: Checkers should repeat in x
           Given pattern ← checkers_pattern(white, black)
           Then pattern_at(pattern, point(0, 0, 0)) = white
           And pattern_at(pattern, point(0.99, 0, 0)) = white
           And pattern_at(pattern, point(1.01, 0, 0)) = black
           */
        [Test]
        public void Checker_should_repeat_in_x()
        {
            //Assign
            var pattern = new Checker(White, Black);
            //Assert
            pattern.ColourAt(Point(0, 0, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(0.99, 0, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(1.01, 0, 0)).Should().BeEquivalentTo(Black);
        }
        /*

           Scenario: Checkers should repeat in y
           Given pattern ← checkers_pattern(white, black)
           Then pattern_at(pattern, point(0, 0, 0)) = white
           And pattern_at(pattern, point(0, 0.99, 0)) = white
           And pattern_at(pattern, point(0, 1.01, 0)) = black
           */
        [Test]
        public void Checker_should_repeat_in_y()
        {
            //Assign
            var pattern = new Checker(White, Black);
            //Assert
            pattern.ColourAt(Point(0, 0, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(0, 0.99, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(0,1.01, 0)).Should().BeEquivalentTo(Black);
        }
        /*
           Scenario: Checkers should repeat in z
           Given pattern ← checkers_pattern(white, black)
           Then pattern_at(pattern, point(0, 0, 0)) = white
           And pattern_at(pattern, point(0, 0, 0.99)) = white
           And pattern_at(pattern, point(0, 0, 1.01)) = black
         */
        [Test]
        public void Checker_should_repeat_in_Z()
        {
            //Assign
            var pattern = new Checker(White, Black);
            //Assert
            pattern.ColourAt(Point(0, 0, 0)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(0, 0, 0.99)).Should().BeEquivalentTo(White);
            pattern.ColourAt(Point(0, 0, 1.01)).Should().BeEquivalentTo(Black);
        }
    }
}
