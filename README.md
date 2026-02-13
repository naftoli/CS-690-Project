# CS-690-Project

Software Engineering Project

## Project Scenario

A Day in the Life of Sarah

### Morning

Sarah sits at the kitchen table, sipping her coffee and staring at the ingredients in her fridge.There’s a chicken breast, some spinach, and a half-empty jar of pesto. “What can I make with this?” she wonders.

She remembers seeing a recipe online last week that used similar ingredients, but shedidn’tsave it. Flipping through herphone’sbrowser history feels like a chore, so she gives up and decides to make a sandwich instead.

### Late Morning

At work, Sarah chats with her coworker, Amanda, who mentions a great lasagna recipe they tried over the weekend. “Send it to me!” Sarah says enthusiastically. But when Amanda emails her the recipe, Sarah files it away in her inbox and forgets about it.

### Lunch

Over lunch, Sarah thinks about how many recipesshe’ssaved in random places—screenshots, bookmarked websites, scraps of paper. “I need to organize these somehow,” she mutters to herself.

She writes “Organize recipes” on a sticky note, but itends upburied under a pile of other notes by the end of the day.

### Afternoon

On her way home, Sarah stops by the grocery store. She wants to try something new for dinner but doesn’t know what to buy. Without a plan, she ends up buying the same staples: pasta, tomatoes, and ground beef.

Back home, she realizes she already had pasta and tomatoes in the pantry. “Why do I always overbuy?” she sighs.

### Evening

Sarah tries to find a recipe for dinner but quickly gets frustrated by the clutter on her phone and in her kitchen. There are loose papers with half-written recipes, random screenshots, and a few cookbooks she barely uses.

She ends up making spaghetti—again—and promises herself she’llfigure out a better system for her recipes. But the day ends with the same disorganized stack of notes.

## Business Requirement

Create a console based Recipe Saver Application to manage all Recipes in one convenient location.

## Use Cases

- UC1: Login
  - FR1: create credentials (username / password) if first time user
  - FR2: save to file
  - FR3: enter username / password to login
  - FR4: program authenticates username / password against file
  - FR5: on success, show main menu
  - FR6: on failure show error message with ability to retry
  - NFR1: Login process should be simple and intuitive
  - NFR2: App should be always available with or without internet connectivity

- UC2: Find Recipe
  - FR1: enable user to search for recipe by term or name
  - FR2: find recipe based on search
  - NFR1: recipe should be found quickly regardless of how many recipes there are
  - NFR2: feature should be simple and intuitive

- UC2: Manage Recipes
  - FR1: Add Recipe by entering name, list of ingredients, and instructions
  - FR2: Delete Recipe by using Find Recipe feature
  - FR3: Edit Recipe by using Find Recipe feature and then choosing to edit the name or ingredients or instructions
  - FR4: All changes need to be saved
  - NFR1: process should be smooth and intuitive
