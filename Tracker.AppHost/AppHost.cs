var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.Tracker_Api>("tracker-api");

builder.AddProject<Projects.Tracker_Web>("tracker-web");

builder.AddProject<Projects.Tracker_Hyb>("tracker-hyb");

builder.AddProject<Projects.Tracker_App>("tracker-app");

builder.Build().Run();
