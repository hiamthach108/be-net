# be-net

## Cấu trúc:
1. Application:
  - Chứa các logic Business: Controllers, DTOs, Services, Shared,...
2. Core:
  - Chứa các Service tái sử dụng lại trong project: Mail, GCS(Google Cloud Storage), Jwt, ...
3. Domain:
  - Chứa các Entity, Use Case.
4. Infrastructure:
  - Làm việc liên quan tới Database
  - Chứa các Repository, Database Init, Cache