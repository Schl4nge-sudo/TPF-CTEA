using System;
using System.Collections.Generic;

namespace tpfinal
{
	
	public class DecisionData
	{
		private Pregunta question;
		public Pregunta Question {
			get {
				return question;
			}
		}
		
		private  Dictionary<string, int> predictions;
		public Dictionary<string, int> Predictions {
			get {
				return predictions;
			}
		}
		
		public DecisionData(Pregunta question)
		{
			this.question = question;
		}
		
		public DecisionData(Dictionary<string, int> predictions)
		{
			this.predictions = predictions;
		}
		
		
		private string imprimirPredicciones()
		{
			//A nicer way to print the predictions.
			double total = 0.0;
			foreach (var lbl in predictions.Keys)
				total += predictions[lbl];
			string probs = "";
			foreach (var lbl in predictions.Keys)
				probs += lbl + ":" + (predictions[lbl] / total * 100) + "% ";
			return probs;
		}
		
		
		public override string ToString()
		{
			if (question != null) {
				
				return question.ToString();
				
			} else {
				
				return imprimirPredicciones();
			}
		}
		
	}
}
