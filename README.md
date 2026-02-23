# CS-690-Project

Software Engineering Project

## Project Scenario

A Day in the Life of Sarah

### Morning

Sarah sits at the kitchen table, sipping her coffee and staring at the ingredients in her fridge.There’s a chicken breast, some spinach, and a half-empty jar of pesto. “What can I make with this?” she wonders.

She remembers seeing a recipe online last week that used similar ingredients, but she didn’t save it. Flipping through her phone’s browser history feels like a chore, so she gives up and decides to make a sandwich instead.

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
  - FR1: [create credentials (username / password) if first time user](https://github.com/naftoli/CS-690-Project/issues/1)
  - FR2: [save to file](https://github.com/naftoli/CS-690-Project/issues/2)
  - FR3: [enter username / password to login](https://github.com/naftoli/CS-690-Project/issues/3)
  - FR4: [program authenticates username / password against file](https://github.com/naftoli/CS-690-Project/issues/4)
  - FR5: [on success, show main menu](https://github.com/naftoli/CS-690-Project/issues/5)
  - FR6: [on failure show error message with ability to retry](https://github.com/naftoli/CS-690-Project/issues/6)
  - NFR1: [Login process should be simple and intuitive](https://github.com/naftoli/CS-690-Project/issues/7)
  - NFR2: [App should be always available with or without internet connectivity](https://github.com/naftoli/CS-690-Project/issues/8)

- UC2: Find Recipe
  - FR1: [enable user to search for recipe by term or name](https://github.com/naftoli/CS-690-Project/issues/9)
  - FR2: [find recipe based on search](https://github.com/naftoli/CS-690-Project/issues/10)
  - NFR1: [recipe should be found quickly regardless of how many recipes there are](https://github.com/naftoli/CS-690-Project/issues/11)
  - NFR2: [feature should be simple and intuitive](https://github.com/naftoli/CS-690-Project/issues/12)

- UC2: Manage Recipes
  - FR1: [Add Recipe by entering name, list of ingredients, and instructions](https://github.com/naftoli/CS-690-Project/issues/13)
  - FR2: [Delete Recipe by using Find Recipe feature](https://github.com/naftoli/CS-690-Project/issues/14)
  - FR3: [Edit Recipe by using Find Recipe feature and then choosing to edit the name or ingredients or instructions](https://github.com/naftoli/CS-690-Project/issues/15)
  - FR4: [All changes need to be saved](https://github.com/naftoli/CS-690-Project/issues/16)
  - NFR1: [process should be smooth and intuitive](https://github.com/naftoli/CS-690-Project/issues/17)
