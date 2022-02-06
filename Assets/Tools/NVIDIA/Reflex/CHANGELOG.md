# Changelog
All notable changes to this package will be documented in this file.

## [Unreleased]

## [1.4.1-alpha.2] - 2021-06-07
### Fixed
- Only start main thread calls once the first render event is seen, resolves issues with D3D11_CREATE_DEVICE_SINGLETHREADED

## [1.4.1-alpha.1] - 2021-04-21
### Added
- Support for bUseMarkersToOptimize
- Support for Unity DX12 v6 API interface

### Changed
- Switched to allocated event ID's
- Switched to R465 interfaces

### Fixed
- Clear all buffers to zero prior to filling

## [1.4.0-alpha.1] - 2021-04-20
### Added
- Package as tarball for easy distribution/integration
- Initial documentation for package
- Support multiple camera with a first and last camera option on the Reflex component
- Custom editor for Reflex component (hide/show options based on isFirstCamera)
- Support for dynamic management of additional Reflex components to support legacy render pipeline

### Changed
- Switched to enum return codes for cleaner error handling
- Attach/detach command buffers on enable/disable
- Start of rendering command buffer attached as first command buffer for camera event

### Fixed
- Cleanup on plugin DLL shutdown

## [1.0.0-alpha] - 2020-11-05
### Added
- Initial release
