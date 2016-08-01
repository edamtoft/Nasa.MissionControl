using System;
using System.Collections.Generic;
using Nasa.MissionControl.IO;
using Nasa.MissionControl.Mission;

namespace Nasa.MissionControlTests.Mission.Mocks
{
  class MockRoverView : IView<IList<Rover>>
  {
    public IList<Rover> Rovers;

    public void Initialize(IList<Rover> rovers)
    {
      Rovers = rovers;
    }

    public void Update(Action<IList<Rover>> updateView)
    {
      updateView(Rovers);
    }
  }
}
