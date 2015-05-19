using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TriviaMaze.com.teamrc.util {
    public interface I_Collidable {
        String getType();

        String[] getCollisionTypes();

        Point getPosition();

        void collidedWith(I_Collidable c);
    }
}
