# Assist a Purchase

Philips offers monitoring solutions, as [seen here](https://www.philips.co.in/healthcare/solutions/patient-monitoring/continuous-patient-monitoring-systems).

This project assists in selecting and delivering the right solution.
It serves both the customer as well as Philips personnel.

It has two segments:

1. The API - Both GUI and non-GUI clients can consume this.
1. The GUI, which is one client of the API

About the clients:

- GUI-clients are for standalone use of this product.
- Non-GUI clients are other systems that need to integrate with this product.

This project implements automated tests, which behave as a non-GUI client.

## [Segment 1] Web API

### Respond to questions

The web-service must respond with answers to questions in a stateless manner.
This is a technique for scaling your solution.
The server’s resource-requirements are greatly simplified
when it doesn’t need to keep track of every client’s previous question.

Design a hierarchy of interactions.
At the start of a search, clients need to know the broad categories on offer.
Once the category is clear, clients get interested in performance and specification.
Clients will then focus on one or two models.

Do not impose this hierarchy.
A repeat-visitor could be specific about the model at the start itself.

### Configure

The web-service shall offer interfaces for configuration:

- Read available models
- Add new models
- Remove obsolete ones.

### Follow the leads

It shall offer an interface to alert subscribers when a user selects a model.
A possible subscriber could be the sales department of the region, for example.
Optionally, extend the web-service to cater to other languages as well.

### REST API guidelines

- [Introductory guide](https://stackoverflow.blog/2020/03/02/best-practices-for-rest-api-design/)
- [Microsoft REST API guideline](https://github.com/microsoft/api-guidelines/blob/vNext/Guidelines.md)

## [Segment 2] User-interface

### User-story: Conversation

As a first-time visitor, I need to have a conversation about the
Continuous Monitoring devices offered.
As Philips, we need to automate this conversation with a chat-bot.
Our sales personnel can then focus on the customer's needs.

Example: The chat-bot starts about broad categories in clinical terms,
after which it comes down to technical specifications and models.

Acceptance Criteria:

- At every stage, the maximum number of choices presented is four
- It shall be possible to start the conversation at any stage

### User-story: Configuration

As Operations, I need to update the models as per the catalog.

Example: We release new Variants and Models,
while older ones go obsolete.

Acceptance Criteria:

- Updates to the models are visible in customer-conversations

### User-story: Sales leads

As Sales, I need to follow-up with customers
who have expressed interest in a particular model.

Example: I receive an e-mail when a customer wants a human interface.

Acceptance Criteria:

- The e-mail must have sufficient detail to contact the correct person
on the customer-side.
