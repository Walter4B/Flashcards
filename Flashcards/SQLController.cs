﻿namespace Flashcards
{
    internal class SQLController
    {
        FlashcardsController flashcardController = new FlashcardsController();
        internal void CreateFlashcardStack()
        { }

        internal void DeleteFlashcardStack()
        { }

        internal void CreateFlashCard()
        { }

        internal void DeleteFlashCard()
        { }

        internal void ShowStacks()
        { }

        internal void ShowFlashcardsInStack()
        { }

        internal void UpdateStack()
        { }

        internal void UpdateFlashcardInStack()
        { }

        internal void StudySession()
        {
            int score = flashcardController.TestSkill(); //TODO
        }
    }
}
