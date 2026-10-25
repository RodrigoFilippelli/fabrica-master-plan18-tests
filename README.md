# fabrica-master-plan18-tests

Isolated synthetic acceptance fixture for the Fabrica Master pipeline.
This is not the FabricaMaster application and contains no customer data or credentials.

## Local validation

Requires .NET 8 SDK and Docker.

```sh
dotnet test tests/Homolog.Fixture.Tests/Homolog.Fixture.Tests.csproj --configuration Release
docker build --tag plan18-fixture:local .
docker run --rm --publish 127.0.0.1:8080:8080 plan18-fixture:local
```

GET /health returns HTTP 200 and exactly:

```json
{"status":"healthy","service":"plan18-synthetic-fixture"}
```

Unknown routes return 404. The container runs as non-root on port 8080.

## Acceptance boundaries

The build job runs integration tests, builds a container and checks its health
contract on the runner. Pull requests do not publish images or deploy anything.
After an authorized merge, main pushes publish a SHA-tagged image to GHCR;
image publication has not yet been homologated.

The previous Node-only demonstrative SBOM step is replaced by real .NET tests
and container smoke validation; this fixture does not claim SBOM generation.
The existing E2E workflow is only a skeleton and has not been dispatched.
CI here does not validate FabricaMaster or its authenticated external SSO flows.

Keep main protected: reviews, CODEOWNERS, build and fabrica/four-eyes checks
are required. Do not forge a passing status or bypass human review.
No webhook, environment configuration or deployment is authorized by this PR.
Do not disable Cloudflare Access or use personal credentials in Actions.
