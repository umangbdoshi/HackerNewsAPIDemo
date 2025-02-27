**Hacker News API Integration**
This is a RESTful API that retrieves the top n best stories from the Hacker News API, sorted by score.

**Assumptions**
•	No authentication required for Hacker News API.
•	N is expected to be a valid integer
•	Caching is used for individual stories for up to 15 minutes.

**Enhancements**
- There can be retry mechanism implemented to handle failed request to Hacker Rank API (e.g., using Polly).
- Add pagination for the best stories list.
-  Improve handling of API errors (e.g., timeouts, rate limits).
- Authentication/Authorization can be implemented for securing the endpoint
- There is a scope to write a Unit tests which can cover business logic and caching logics

**Getting Started**
1. Clone the repo:
   git clone https://github.com/your-username/hacker-news-api.git

2. Run the application

API will be available at http://localhost:5000
Swagger Url: https://localhost:7016/swagger/index.html
