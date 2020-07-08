using System;

namespace BugTracker.Models
{
  public class Severity
  {
      public Guid Id { get; set; }
      public string Name { get; set; }
      public DateTime createdAt { get; set; }
      public DateTime updatedAt { get; set; }
  }
}