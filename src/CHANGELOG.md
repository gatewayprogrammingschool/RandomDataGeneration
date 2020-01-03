# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [1.1.0] - Added Options and Extensions

- Reusable Option objects:
  - `Options`: Basic for types that don't have a range
  - `RangeOptions`: For types that have a range
- Extension methods on `System.Random` that can accept options or typed parameters.
- `IResettable` allows Generators (`IDataGenerator`) to be reset.

## [1.0.3] - Packages Cleanup

### Added

- CHANGELOG.md

## [1.0.2] - Initial Release

### Added

- Base Data Providers for Given Names, Surnames and Domain Names.
- Abstractions for Data Providers (`IDataProvider`) and Data Generators (`IDataGenerator`).
- Simple Generators for Integers (`Int32`), Doubles (`double`), Decimals (`decimal`), Guids (`Guid`), DateTimes (`DateTime`), Colors (`System.Drawing.Color`) and Sequences (`Int32`).
- Simple Generators for Given Names, Surnames and Full Names.
- Simple Email Address Generator
- Simple Record (POCO) Generator
  - Define properties in a map and apply to the Simple Record.
- Dependency Injection

[Unreleased]: https://github.com/gatewayprogrammingschool/RandomDataGeneration/compare/1.1.0...HEAD
[1.1.0]: https://github.com/gatewayprogrammingschool/RandomDataGeneration/compare/1.0.3...1.1.0
[1.0.3]: https://github.com/gatewayprogrammingschool/RandomDataGeneration/compare/1.0.2...1.0.3
[1.0.2]: https://github.com/gatewayprogrammingschool/RandomDataGeneration/releases/tag/1.0.2
