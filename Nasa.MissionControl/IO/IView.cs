using System;

namespace Nasa.MissionControl.IO
{
  /// <summary>
  /// Represents a view of the given model
  /// </summary>
  /// <typeparam name="TModel"></typeparam>
  public interface IView<TModel>
  {
    void Initialize(TModel model);

    void Update(Action<TModel> updateView);
  }
}
