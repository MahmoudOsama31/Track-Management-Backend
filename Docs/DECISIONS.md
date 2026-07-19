# DECISIONS.md

## 1. What did AI generate for you, and what did you write or modify yourself?

AI tools (primarily Claude and ChatGPT) were used throughout the project as coding assistants. They also assisted in preparing the project documentation (README.md and DECISIONS.md), which I reviewed, verified, and finalized.

- **Backend**: AI helped generate boilerplate — repetitive CRUD controller actions, DTOs,
  EF Core entity configurations, FluentValidation validators, and mapping profiles. I designed
  the overall project architecture (Clean Architecture split across Domain / Application /
  Infrastructure / API), designed the business flow (track lifecycle, distribution rules,
  status transitions), and implemented the business rules myself. I reviewed every
  AI-generated file, fixed integration issues between layers (DI registration, EF migrations,
  JWT middleware wiring), and manually tested all endpoints.
- **Frontend**: AI generated most of the initial Angular implementation — components, routing,
  services, and basic template markup for the Track List and Track Detail views. I reviewed
  every generated file, corrected data-binding and state issues, fixed API integration
  (request/response shapes not matching the backend DTOs), and adjusted the filtering and
  navigation behavior to match the required UX.
- Manual end-to-end testing (running the API, calling endpoints, and exercising the UI against
  the live API) was done by me, not by AI.

## 2. What security issues did you find (or introduce) in the AI-generated code?

Issues found in the first AI-generated pass:

- **Missing authorization** on endpoints that should have been protected — the initial
  scaffold left some mutating routes open. Fixed by applying `[Authorize]` and verifying JWT
  validation is enforced on the intended protected endpoint(s).
- **Over-posting risk** — early controller actions bound directly to EF entities instead of
  request DTOs, which would have let a caller set fields like `id` or `status` directly. Fixed
  by introducing dedicated request DTOs for every write operation and mapping explicitly to
  entities.
- **Missing input validation** — several endpoints had no validation on required fields,
  string lengths, or enum values (e.g. track status, ISRC format). Fixed by adding
  FluentValidation validators and returning structured 400 responses.
- **Leaking exception details** — an early exception handler returned the raw exception
  message and stack trace to the client. Fixed by adding a global exception-handling
  middleware that logs the full exception server-side and returns a generic, safe error
  response to the client.

## 3. One thing the AI got wrong that you had to fix

The AI's first implementation of the track distribution flow (`POST /api/tracks/{id}/distribute`)
updated the track/distribution status *before* validating the request and did not check whether
a distribution to a given DSP already existed. This meant a track could be re-submitted to the
same DSP multiple times, creating duplicate `TrackDistribution` records, and a failed/invalid
request could still leave the track in an inconsistent status. This was wrong because the
business rule requires idempotent submissions per DSP and a status change only after the
request is confirmed valid. I fixed it by validating the request and checking for an existing
pending/live distribution first, and only updating status after that check succeeds.
