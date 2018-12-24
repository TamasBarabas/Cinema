# Cinema

## Design
Multiple layers of abstraction is used, which makes the design closer to a monolit than a microservice. This is intentional, because in microservice api good practices are less important.

applications.json has 2 connection strings, CinemaDatabase(in comment) and CinemaDatabaseSqLite. If CinemaDatabase is uncommented, the sql server database is used. otherwise SqLite database is created and seeded from DAL\DbSeed.cs file.

Repository unit tests use InMemory provider, which is also seeded from DAL\DbSeed.cs file.

## Todo
- I have'nt added unit tests for the service and presentation layer, because there is no logis in it. However integration test could bew added
- Seed the enum into database

