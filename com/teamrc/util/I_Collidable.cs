using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * An interface used by objects that can be collided.
 */
namespace TriviaMaze.com.teamrc.util {
    public interface I_Collidable {
        /**
         * @returns The Component's Rectangle image
         */
        Rectangle getImage();

        /**
         * @returrns the Component's Brush color.
         */
        Brush getColor();
        
        /**
         * @returns a String representation of the Component's type.
         */
        String getType();

        /**
         * @returns an array of types the object can collide with.
         */
        String[] getCollisionTypes();

        /**
         * @returns a Point representing the Component's position.
         */
        Point getPosition();

        /**
         * @returns a single character String representing the Component.
         */
        String toString();
    }
}
