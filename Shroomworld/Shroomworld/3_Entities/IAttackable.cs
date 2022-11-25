using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shroomworld
{
    internal interface IAttackable
    {
        public event EventHandler<AttackEventArgs> AttackCompleted;

        public void BeginAttack();
        public void ContinueAttack();

        protected void OnAttackCompleted();
    }
}
