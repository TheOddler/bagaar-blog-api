# A simple blogging API for Bagaar

# Instructions

We would like you to build a small "blog" JSON REST-API in ASP.NET Core.
There should be Posts with a `title`, `content` and created timestamp.
Title and content are required fields and title has a max length of 50 characters.
Each post can have multiple Comments which only have a `content` and a timestamp.
For storing the data we would like you to use Entity Framework Core (SQlite driver is fine).
It should be possible to:
- Create a Post
- Read a Post
- Delete a Post
- Update a Post
- Add a Comment to a Post
- Fetch all Posts sorted by recent first
- Filter posts on partial match of the title

# Docs

## Create a Post

`POST api/posts` with body:

    {
        "title": "<title>",
        "content": "<content>"
    }

## Read a Post

`GET api/posts/<id>`

## Delete a Post

`DELETE api/posts/<id>`

## Update a Post

`PUT api/posts/<id>` with body:

    {
        "id": <id>
        "title": "<title>",
        "content": "<content>"
    }

## Add a Comment to a Post

`POST api/comments/` with body:

    {
        "postId": <post id>,
        "content": "<content>"
    }

or

`POST api/posts/<post id>/comments` with body:

    {
        "content": "<content>"
    }

Optionally a `postId` can be added to the body which must match the `post id` in the url.

## Fetch all Posts sorted by recent first

`GET api/posts`

## Filter posts on partial match of the title

`GET api/posts?title=<partial title>`

## Extras

### Get comments

`GET api/comments`

`GET api/comments?postId=<post id>`

`GET api/posts/<post id>/comments`

### Get specific comment

`GET api/comments/<id>`