# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

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

[Unreleased]: https://github.com/gatewayprogrammingschool/RandomDataGeneration/compare/1.0.2...HEAD
[1.0.2]: https://github.com/gatewayprogrammingschool/RandomDataGeneration/releases/tag/1.0.2
