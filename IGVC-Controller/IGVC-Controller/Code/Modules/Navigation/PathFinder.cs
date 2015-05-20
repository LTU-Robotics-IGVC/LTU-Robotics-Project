﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGVC_Controller.Code.Modules.Navigation
{
    class PathFinder : IModule
    {
        public PathFinder() : base()
        {
            this.modulePriority = 61;
        }

        public override System.Windows.Forms.Form getEditorForm()
        {
            return new PathFinderEditor();
        }
    }
}
